namespace SpaceInvaders.Models
{
    public class Position(int x, int y)
    {
        public int x { get; set; } = x;
        public int y { get; set; } = y;
        public (int, int) ToTuple()
        {
            return (x, y);
        }
        public static Position FromTuple((int, int) tuple)
        {
            return new Position(tuple.Item1, tuple.Item2);
        }
        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}
