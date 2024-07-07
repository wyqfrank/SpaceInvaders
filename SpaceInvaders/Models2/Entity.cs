namespace api.Models
{
    public abstract class Entity
    {
        private int x;
        private int y;
        private int speed;
        public Entity(int x = 0, int y = 0, int speed = 10)
        {
            this.x = x;
            this.y = y;
            this.speed = speed;
        }
    }
    public class Player : Entity
    {
        public string connectionId;
        public Player(string connectionId, int x, int y, int speed) : base(x, y, speed)
        {
            this.connectionId = connectionId;
        }
    }
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