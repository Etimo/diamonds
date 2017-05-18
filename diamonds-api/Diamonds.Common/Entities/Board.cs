using System;
using System.Collections.Generic;
using Diamonds.Common.Models;

namespace Diamonds.Common.Entities
{
    public class Board
    {
        public IEnumerable<BoardBot> Bots { get; set; }
        public IEnumerable<Position> Diamonds { get; set; }
        public string BoardId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

}
