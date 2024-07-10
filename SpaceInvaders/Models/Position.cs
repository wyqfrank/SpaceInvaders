namespace SpaceInvaders.Models
{
    public class Position(int x, int y)
    {
        private int x { get; set; } = x;
        private int y { get; set; } = y;

        public (int, int) ToTuple() // getter
        {
            return (x, y);
        }
        public static Position FromTuple((int, int) tuple) // setter
        {
            return new Position(tuple.Item1, tuple.Item2);
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}
