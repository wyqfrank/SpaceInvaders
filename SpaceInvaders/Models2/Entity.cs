using System.Collections;

namespace SpaceInvaders.Models2
{
    public abstract class Entity
    {
        protected int x;
        protected int y;
        public int X => x;
        public int Y => y;
        public int speed;
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
        public void MovePlayer(string input)
        {
            switch(input)
            {
                case "w":
                    y+=speed;
                    break;
                case "s":
                    y+=speed;
                    break;
                case "a":
                    x-=speed;
                    break;
                case "d":
                    x+=speed;
                    break;
            }
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