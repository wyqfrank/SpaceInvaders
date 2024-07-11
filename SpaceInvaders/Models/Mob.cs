namespace SpaceInvaders.Models
{
        public class Mob : Entity
    {
        public bool canShoot;
        public Mob(int x, int y, int speed, bool canShoot) : base(x, y, speed)
        {
            this.canShoot = canShoot;
        }
    }
}
