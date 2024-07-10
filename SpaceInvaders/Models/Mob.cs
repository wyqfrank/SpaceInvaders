namespace SpaceInvaders.Models
{
        public class Mob : Entity
    {
        public Type type;
        public bool canShoot;
        public Mob(int x, int y, int speed, Type type, bool canShoot) : base(x, y, speed)
        {
            this.type = type;  
            this.canShoot = canShoot;
        }
    }
}
