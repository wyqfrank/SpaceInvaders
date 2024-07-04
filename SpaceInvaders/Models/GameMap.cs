namespace SpaceInvaders.Models
{
    public class GameMap
    {
        private char[][] mapLayout { get; }


        public GameMap(Player[] players, Mob[] Mobs, int width, int height) {
            mapLayout = new char[height][];
        }

        public char[][] GetMapLayout()
        {
            return mapLayout;
        }

        public void SetMapLayout()
        {
             
        }

    }
}
