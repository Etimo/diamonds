﻿using System;
using Diamonds.Common.Storage;
using Diamonds.Common.Enums;
using Diamonds.Common.GameEngine.Move;
using Diamonds.Common.GameEngine.GameObjects;
using System.Linq;
using Diamonds.Common.Entities;
using Diamonds.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Diamonds.Common.GameEngine.DiamondGenerator;

namespace Diamonds.GameEngine
{
    public class MoveService : IMoveService
    {
        protected readonly IStorage _storage;
        protected readonly IDiamondGeneratorService _boardDiamondManager;
        protected readonly IGameObjectGeneratorService _boardObjectGenerator;

        public MoveService(IStorage storage,
        IDiamondGeneratorService boardDiamondManager,
        IGameObjectGeneratorService boardObjectGenerator)
        {
            _storage = storage;
            _boardDiamondManager = boardDiamondManager;
            _boardObjectGenerator = boardObjectGenerator;
        }

        // TODO: Consider moving the call to _boardDiamondManager away from this class
        private void regenerateBoardObjects(Board board)
        {
            board.GameObjects = new List<BaseGameObject>();
            board.Diamonds = _boardDiamondManager.GenerateDiamondsIfNeeded(board);
            _boardObjectGenerator?
                .getCurrentObjectGenerators()
                .ForEach(gog =>
                    board.GameObjects.AddRange(gog.RegenerateObjects(board)));
        }

        public async Task<MoveResultCode> MoveAsync(string boardId, string botName, Direction direction)
        {
            var board = await _storage.GetBoardAsync(boardId);
            var resultCode = await PerformMoveAndUpdateBoardAsync(board, botName, direction);

            if (resultCode != MoveResultCode.Ok)
            {
                return resultCode;
            }

            if (_boardDiamondManager.NeedToGenerateDiamonds(board))
            {
                regenerateBoardObjects(board);
            }
            await _storage.UpdateBoardAsync(board);

            return MoveResultCode.Ok;
        }

        private async Task<MoveResultCode> PerformMoveAndUpdateBoardAsync(Board board, string botName, Direction direction)
        {
            var bot = board.Bots.SingleOrDefault(b => b.Name == botName);

            if (bot == null) return MoveResultCode.InvalidMove;

            if (bot.IsGameOver())
            {
                await RemoveBotAsync(bot, board);
                return MoveResultCode.InvalidMove;
            }

            var previousPosition = bot.Position;
            var attemptedNextPosition = CalculateNewPosition(previousPosition, direction);
            var canMoveToAttemptedNextPosition = CanMoveToPosition(board, bot, attemptedNextPosition);

            if (canMoveToAttemptedNextPosition == false)
            {
                return MoveResultCode.CanNotMoveInThatDirection;
            }

            AttemptPickUpDiamond(attemptedNextPosition, board, bot);
            AttemptDeliverInBase(attemptedNextPosition, bot);

            bot.Position = attemptedNextPosition;
            AttemptTriggerGameObject(board,direction,attemptedNextPosition, bot,_boardDiamondManager);

            // update timers on bot
            bot.NextMoveAvailableAt = DateTime.UtcNow.AddMilliseconds(board.MinimumDelayBetweenMoves);

            return MoveResultCode.Ok;
        }

        private void AttemptTriggerGameObject(Board board,Direction direction,Position attemptedNextPosition, BoardBot bot,IDiamondGeneratorService generator)
        {
            var gameObject = board.GameObjects.Where(gf => gf.Position.Equals(attemptedNextPosition)).
            DefaultIfEmpty(new DoNothingGameObject()).FirstOrDefault();
            gameObject.PerformInteraction(board,bot,direction,generator);

        }

        private async Task RemoveBotAsync(BoardBot bot, Board board)
        {
            var removed = board.Bots.Remove(bot);
            await _storage.UpdateBoardAsync(board);
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
            if (positionHasDiamond == false)
            {
                return;
            }

            DiamondPosition diamond = board.Diamonds.First(p => p.Equals(position));
            bool hasEnoughSpace = bot.Diamonds <= (5 - diamond.Points);
            if (hasEnoughSpace == false)
            {
                return;
            }

            bot.Diamonds += diamond.Points;
            board.Diamonds = board.Diamonds
                .Where(p => p.Equals(position) == false)
                .ToList();

            // TODO: Remember to generate new diamonds when the total diamond count is too low. We don't know yet where this should be in the code.
        }

        private bool CanMoveToPosition(Board board, BoardBot bot, Position position)
        {
            return PositionIsInBoard(position, board)
                && PositionIsOpponentBase(position, bot.BotId, board.Bots) == false
                && board.IsPositionBlocked(position) == false; //Includes bots and GameObjects which return true for blockable.
        }

        private bool PositionHasBot(Position position, IEnumerable<BoardBot> bots)
        {
            return bots.Any(b => b.Position.Equals(position));
        }

        private bool PositionIsOpponentBase(Position position, string selfBotId, IEnumerable<BoardBot> allBots)
        {
            var opponentBots = allBots.Where(b => b.BotId != selfBotId);

            var positionIsOpponentBase = opponentBots.Any(b => b.Base.Equals(position));

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

        private Position CalculateNewPosition(Position previousPosition, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return new Position(previousPosition.X, previousPosition.Y - 1);

                case Direction.South:
                    return new Position(previousPosition.X, previousPosition.Y + 1);

                case Direction.East:
                    return new Position(previousPosition.X + 1, previousPosition.Y);

                case Direction.West:
                    return new Position(previousPosition.X - 1, previousPosition.Y);

                default:
                    throw new ArgumentException($"Argument direction har invalid value { direction }");
            }
        }
    }
}
