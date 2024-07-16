namespace SpaceInvaders.Models
{
    public class MobPatternGenerator
    {
        private Random random = new Random();

        public List<Mob> GenerateRandomMobPattern(int canvasWidth, int canvasHeight, int numMobs)
        {
            List<Mob> mobs = new List<Mob>();

            for (int i = 0; i < numMobs; i++)
            {
                int x = random.Next(canvasWidth);
                int y = random.Next(canvasHeight);
                int speed = random.Next(5, 15); 
                bool canShoot = random.Next(2) == 0; 

                Mob mob = new Mob(x, y, speed, canShoot);
                mobs.Add(mob);
            }

            return mobs;
        }
        public List<Mob> MoveMob(List<Mob> mobs)
        {
            foreach (Mob mob in mobs)
            {
                int directionIndex = random.Next(8);
                Direction randomDirection = (Direction)directionIndex;
                if (randomDirection == Direction.N)
                {
                    mob.y -= 10;
                }
                else if (randomDirection == Direction.W)
                {
                    mob.x += 10;
                }
                else if (randomDirection == Direction.E)
                {
                    mob.x -= 10;
                }
                else if (randomDirection == Direction.S)
                {
                    mob.y += 10;
                }
                else if (randomDirection == Direction.NW)
                {
                    mob.y -= 10;
                    mob.x += 10;
                }
                else if (randomDirection == Direction.NE)
                {
                    mob.y -= 10;
                    mob.x -= 10;
                }
                else if (randomDirection == Direction.SE)
                {
                    mob.y += 10;
                    mob.x -= 10;
                }
                else if (randomDirection == Direction.SW)
                {
                    mob.y += 10;
                    mob.x += 10;
                }  
            }
        return mobs;
        }

        public Direction randomDirections()
        {
            int directionIndex = random.Next(8); 
            return (Direction)directionIndex;
        }

    }
}