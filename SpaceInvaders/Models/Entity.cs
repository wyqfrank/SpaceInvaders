namespace SpaceInvaders.Models
{
    public abstract class Entity
    {
        public int x;
        public int y;
        public int health;
        public int speed;
        public Entity(int x = 0, int y = 0, int speed = 10, int health = 100)
        {
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.health = health;
        }        
    }
}
