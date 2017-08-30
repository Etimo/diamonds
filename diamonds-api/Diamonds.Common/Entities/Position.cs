namespace Diamonds.Common.Entities
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            var p = obj as Position;
            return p.X == X && p.Y == Y;
        }
    }

}
