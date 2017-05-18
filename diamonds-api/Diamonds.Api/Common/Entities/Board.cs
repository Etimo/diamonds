using System;
using System.Collections.Generic;

namespace Common.Entities
{
    public class Board
    {
        public IEnumerable<Bot> Bots { get; set; }
        public IEnumerable<Position> Diamonds { get; set; }
        public string BoardId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

}
