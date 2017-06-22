using System;
using Diamonds.Common.Entities;

namespace Diamonds.Common.Models
{
    public class BoardBot
    {
        public string Name { get; set; }
        public Position Base { get; set; }
        public Position Position { get; set; }
        public int Diamonds { get; set; }
        public int MillisecondsLeft { get; set; }
        public int Score { get; set; }
        public string BotId { get; set; }
    }

}
