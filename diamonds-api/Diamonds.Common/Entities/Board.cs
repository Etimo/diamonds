using System;
using System.Collections.Generic;
using System.Linq;
using Diamonds.Common.Models;

namespace Diamonds.Common.Entities
{
    public class Board
    {
        public string Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<BoardBot> Bots { get; set; }
        public List<Position> Diamonds { get; set; }

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
            return Bots.Any(item => item.Name.Equals(bot.Name));
        }

        private Position GetRandomPosition()
        {
            return new Position(
                (int)(Width * new Random().NextDouble()),
                (int)(Width * new Random().NextDouble())
            );
        }

        // 1 minute (60 0000 ms) hard coded for now
        public static int TotalGameTime => 60 * 1000;
 
        private BoardBot CreateBoardBot(Bot bot)
        {
            return new BoardBot
            {
                BotId = bot.Id,
                Name = bot.Name,
                Base = GetRandomPosition(),
                Position = GetRandomPosition(),
                Score = 0,
                Diamonds = 0,
                TimeJoined = DateTime.UtcNow,
                MillisecondsLeft = TotalGameTime
            };
        }

        public void AddBot(Bot bot)
        {
            Bots.Add(CreateBoardBot(bot));
        }
    }

}
