namespace Diamonds.Common.Entities
{
    public class DiamondPosition : Position
    {
        public int Points { get; set; }
        public DiamondPosition(int x, int y, int points = 1) : base(x, y)
        {
            Points = points;
        }

        public DiamondPosition(Position p, int points = 1) : this(p.X, p.Y, points)
        {

        }
    }
}