namespace SpaceInvaders.Models
{
    public class GoL
    {
        GameMap gameMap = null!;
        Player[] players = null!;
        Mob[] mobs = null!;

        public GoL(GameMap gameMap)
        {
            this.gameMap = gameMap;
        }

        public void evolve()
        {
            this.gameMap.GetMapLayout();
        }

        public void count_neighbours()
        {
            char [][] layout = this.gameMap.GetMapLayout();
            
        }


    }
}
