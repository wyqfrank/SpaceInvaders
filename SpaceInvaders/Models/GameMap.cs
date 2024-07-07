namespace SpaceInvaders.Models
{
    public class GameMap
    {
        
        private char[][] mapLayout { get; }
        public GameMap(int width, int height) {
            mapLayout = new char[height][];
            for (int i = 0; i < height; i++)
            {
                mapLayout[i] = new char[width];
                for (int j =0; j < width; j++)
                {
                    mapLayout[i][j] = '.';
                }
            }
        }
        public char[][] GetMapLayout()
        {
            return mapLayout;
        }
        public void UpdatePosition(int x, int y, char entity)
        {
             mapLayout[x][y] = entity;
        }

    }
}
