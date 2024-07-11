using SpaceInvaders.Models;

namespace SpaceInvaders.Models
{
    public class GameMap
    {
        private Dictionary<Entity, Tuple<int, int>>[,] MapLayout { get; }
        public GameMap(int width, int height)
        {
            MapLayout = new Dictionary<Entity, Tuple<int, int>>[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    MapLayout[i, j] = new Dictionary<Entity, Tuple<int, int>>();
                }
            }
        }
        public Dictionary<Entity, Tuple<int, int>>[,] GetMapLayout()
        {
            return MapLayout;
        }

        public void AddEntity(Entity entity, int x, int y)
        {
            if (entity is Mob || entity is Player)
            {
                if (x >= 0 && x < MapLayout.GetLength(1) && y >= 0 && y < MapLayout.GetLength(0))
                {
                    MapLayout[y, x].Add(entity, Tuple.Create(x, y));
                }
                else
                {
                    Console.WriteLine("Position out of bounds.");
                }
            }
            else
            {
                Console.WriteLine("Only Mob and Player entities can be added to the map.");
            }
        }

        public void PrintMap()
        {
            for (int i = 0; i < MapLayout.GetLength(0); i++)
            {
                for (int j = 0; j < MapLayout.GetLength(1); j++)
                {
                    Console.Write("[");
                    foreach (var entity in MapLayout[i, j])
                    {
                        Console.Write(entity.ToString() + " ");
                    }
                    Console.Write("]");
                }
                Console.WriteLine();
            }
        }

    }
}