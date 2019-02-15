using System;
using System.Collections.Generic;
using System.Linq;
using Diamonds.Common.GameEngine.GameObjects;
using Diamonds.Common.Models;

namespace Diamonds.Common.Entities
{
    public class Board
    {

        static Random _random = new Random();
        public List<BaseGameObject> GameObjects  { get;set;} = new List<BaseGameObject>();
        public string Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<BoardBot> Bots { get; set; }
        public List<Position> Diamonds { get; set; }
        public int MinimumDelayBetweenMoves { get; set; } = DefaultMinimumDelayBetweenMoves;
        private const int EdgePositionsToSkipForBase = 2;
        private Position[] _potentialBasePosition;
        private Position[] PotentialBasePositions
        {

            get
            {
                return _potentialBasePosition = _potentialBasePosition ??
                // Top and bottom
                Enumerable.Range(EdgePositionsToSkipForBase, Width - EdgePositionsToSkipForBase).SelectMany(x => new[] {
                    new Position(x, 0),
                    new Position(x, Height - 1)
                })
                // Left and right
                .Concat(
                    Enumerable.Range(EdgePositionsToSkipForBase, Height - EdgePositionsToSkipForBase).SelectMany(y => new[] {
                        new Position(0, y),
                        new Position(Width - 1, y)
                    })
                )
                .ToArray();
            }
        }

        public bool IsFull()
        {
            var size = Width * Height;
            var bots = Bots.Count();
            // Consider >10% of the board to be full
            // E.g. 10x10 with 10 bots is considered full
            return bots > (int)(size * 0.1);
        }

        public bool HasBot(Bot bot)
        {
            return GetBoardBot(bot) != null;
        }

        private BoardBot GetBoardBot(Bot bot)
        {
            return Bots.SingleOrDefault(item => item.Name.Equals(bot.Name));
        }

        public bool CanMove(Bot bot)
        {
            return GetBoardBot(bot).CanMove();
        }

        // 1 minute (60 0000 ms) hard coded for now
        public static int TotalGameTime => (int)TimeSpan.FromMinutes(1).TotalMilliseconds;

        // 100ms delay required between each move, hard coded for now
        public static int DefaultMinimumDelayBetweenMoves = 100;

        private BoardBot CreateBoardBot(Bot bot)
        {

            var basePosition = GetRandomBasePosition();
            return new BoardBot
            {
                BotId = bot.Id,
                Name = bot.Name,
                Base = basePosition,
                Position = basePosition,
                Score = 0,
                Diamonds = 0,
                TimeJoined = DateTime.UtcNow,
            };
        }

        public void AddBot(Bot bot)
        {
            Bots.Add(CreateBoardBot(bot));
        }

        public Position GetRandomBasePosition()
        {
            while (true)
            {
                var randomBasePosition = PotentialBasePositions[_random.Next(PotentialBasePositions.Length)];

                if (IsPositionEmpty(randomBasePosition))
                {
                    return randomBasePosition;
                }
            }
        }

        public Position GetRandomEmptyPosition()
        {
            while (true)
            {
                var randomBoardPosition = GetRandomPosition(Height, Width);
                var canPutDiamondInPosition = IsPositionEmpty(randomBoardPosition);
                if (canPutDiamondInPosition)
                {
                    return randomBoardPosition;
                }
            }
        }

        private bool IsPositionEmpty(Position position)
        {
            var positionHasBot = Bots.Any(b => b.Position.Equals(position));
            var positionHasBase = Bots.Any(b => b.Base.Equals(position));
            var positionHasDiamond = Diamonds.Contains(position);
            var positionHasGameObject = GameObjects.Any(gi => gi.Position.Equals(position));

            return positionHasBot == false
                && positionHasBase == false
                && positionHasDiamond == false
                && positionHasGameObject == false;
        }
        public bool IsPositionBlocked(Position position)
        {
            var positionHasBot = Bots.Any(b => b.Position.Equals(position));
            var positionHasBlockingGameObject =
             this.GameObjects.Any(gi => gi.IsBlocking && gi.Position.Equals(position));

            return //positionHasBot   &&
                   positionHasBlockingGameObject;
        }

        private static Position GetRandomPosition(int height, int width)
        {
            var y = _random.Next(0, height);
            var x = _random.Next(0, width);

            return new Position(x, y);
        }
    }
}
