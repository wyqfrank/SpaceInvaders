namespace SpaceInvaders.Models
{
    public class GameMap
    {
        private char[,] mapLayout;

        private Player[] players ;

        private Mob[] mobs;
        public GameMap(Player[] players, Mob[] Mobs) 
        {
            
        }

        public char[,] GetMapLayout()
        {
            return mapLayout;
        }

        public void SetMapLayout()
        {
             
        }

    }
}
