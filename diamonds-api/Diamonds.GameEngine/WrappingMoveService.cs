using System;
using Diamonds.Common.Storage;
using Diamonds.Common.Enums;
using Diamonds.Common.GameEngine.Move;
using System.Linq;
using Diamonds.Common.Entities;
using Diamonds.Common.Models;
using System.Collections.Generic;
using Diamonds.Common.GameEngine.DiamondGenerator;

namespace Diamonds.GameEngine
{
    /**
    *The teleporting move-service wraps a bot around to the other side of the board
    *when exiting. 
    * As an example a move that would result x> board length will wrap around to x=0, 
    * whereas X <0 will result in x=board.maxx
     */
    public class WrappingMoveService : MoveService
    {

        public WrappingMoveService(IStorage storage, IDiamondGeneratorService boardDiamondManager)
          :base(storage,boardDiamondManager) //TODO: Remove inheritance, all methods are reimplemented.
        {
        }

        public new MoveResultCode Move(string boardId, string botName, Direction direction)
        {
            var board = _storage.GetBoard(boardId);
            var resultCode = PerformMoveAndUpdateBoard(board, botName, direction);

            if (resultCode != MoveResultCode.Ok)
            {
                return resultCode;
            }

            // TODO: Consider moving the call to _boardDiamondManager away from this class
            board.Diamonds = _boardDiamondManager.GenerateDiamondsIfNeeded(board);  //TODO: Better place..
            _storage.UpdateBoard(board);

            return MoveResultCode.Ok;
        }

        private MoveResultCode PerformMoveAndUpdateBoard(Board board, string botName, Direction direction)
        {
            var bot = board.Bots.SingleOrDefault(b => b.Name == botName);
            if (bot == null) return MoveResultCode.InvalidMove;
            if (bot.IsGameOver())
            {
                RemoveBot(bot, board);
                return MoveResultCode.InvalidMove;
            }

            var previousPosition = bot.Position;
            var attemptedNextPosition = CalculateNewPosition(previousPosition, direction,board);
            var canMoveToAttemptedNextPosition = CanMoveToPosition(board, bot, attemptedNextPosition);
            if (canMoveToAttemptedNextPosition == false)
            {
                return MoveResultCode.CanNotMoveInThatDirection;
            }

            AttemptPickUpDiamond(attemptedNextPosition, board, bot);
            AttemptDeliverInBase(attemptedNextPosition, bot);

            bot.Position = attemptedNextPosition;

            // update timers on bot
            bot.NextMoveAvailableAt = 
                DateTime.UtcNow.AddMilliseconds(board.MinimumDelayBetweenMoves);

            return MoveResultCode.Ok;
        }

        private void RemoveBot(BoardBot bot, Board board)
        {
            var removed = board.Bots.Remove(bot);
            _storage.UpdateBoard(board);
        }

        private void AttemptDeliverInBase(Position position, BoardBot bot)
        {
            var positionIsOwnBase = position.Equals(bot.Base);

            if (positionIsOwnBase == false)
            {
                return;
            }

            bot.Score += bot.Diamonds;
            bot.Diamonds = 0;
        }
        
        private void AttemptPickUpDiamond(Position position, Board board, BoardBot bot)
        {
            bool positionHasDiamond = board.Diamonds.Any(p => p.Equals(position));
            bool hasLessThanFiveDiamond = bot.Diamonds < 5;
            bool shouldPickUpDiamond = positionHasDiamond && hasLessThanFiveDiamond;

            if (shouldPickUpDiamond == false)
            {
                return;
            }

            bot.Diamonds += 1;
            board.Diamonds = board.Diamonds
                .Where(p => p.Equals(position) == false)
                .ToList();

            // TODO: Remember to generate new diamonds when the total diamond count is too low. We don't know yet where this should be in the code.
            //In general update method in board, to also regenerate other objects?
        }

        /**
        *Currently untriggerable. 
        *Will be needed once walls, etc, 
        *are incorporated into the board.
         */
        private bool CanMoveToPosition(Board board, BoardBot bot, Position position)
        {
            return PositionIsInBoard(position, board)
                && PositionIsOpponentBase(position, bot.BotId, board.Bots) == false
                && PositionHasBot(position, board.Bots) == false;
        }

        private bool PositionHasBot(Position position, IEnumerable<BoardBot> bots)
        {
            return bots.Any(b => b.Position.Equals(position));
        }

        private bool PositionIsOpponentBase(Position position, string selfBotId, IEnumerable<BoardBot> allBots)
        {
            var opponentBots = allBots.Where(b => b.BotId != selfBotId);

            var positionIsOpponentBase = opponentBots.Any(b => b.Position.Equals(position));

            return positionIsOpponentBase;
        }

        private bool PositionIsInBoard(Position position, Board board)
        {
            return IsInRange(position.X, 0, board.Width - 1)
                && IsInRange(position.Y, 0, board.Height - 1);
        }

        private bool IsInRange(int numberToCheck, int rangeMin, int rangeMax)
        {
            return rangeMin <= numberToCheck && numberToCheck <= rangeMax;
        }

    /**
    *Wraps the provided position around the board if 
    *it is larger or smaller than the board.
    *@param pos Position calculated from previous position
    *@param board The current game board.
     */
    private Position WrapPosition(Position pos,Board board){
        var maxX = board.Width-1;
        var maxY = board.Height-1;
        var currentY = pos.Y > maxY ? 0 : (pos.Y<0 ? maxY : pos.Y);
        var currentX = pos.X > maxX ? 0 : (pos.X<0 ? maxX : pos.X);
        return new Position(currentX,currentY);
    }
        private Position CalculateNewPosition(Position previousPosition, Direction direction,Board board)
        {
            Position IntermediatePosition;
            switch (direction)
            {
                case Direction.North:
                    IntermediatePosition =  new Position(previousPosition.X, previousPosition.Y - 1);
                    break;

                case Direction.South:
                    IntermediatePosition =  new Position(previousPosition.X, previousPosition.Y + 1);
                    break;

                case Direction.East:
                    IntermediatePosition =  new Position(previousPosition.X + 1, previousPosition.Y);
                    break;

                case Direction.West:
                    IntermediatePosition =  new Position(previousPosition.X - 1, previousPosition.Y);
                    break;

                default:
                    throw new ArgumentException($"Argument direction har invalid value { direction }");
            }
            return WrapPosition(IntermediatePosition,board);
        }
    }
}
