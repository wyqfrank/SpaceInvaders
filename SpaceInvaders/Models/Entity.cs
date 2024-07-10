namespace SpaceInvaders.Models
{
    public abstract class Entity
    {
        private int x;
        private int y;
        public int[][] position;
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public int health;
        public Entity(int[][] position, int health) 
        {
            this.position = position;
            this.health = health;
        }
        public int[][] getPosition()
        {
            return this.position;
        }
        public int getHealth(int health)
        {
            return this.health;
        }
        public void setPosition(int[][] newPosition)
        {
            this.position = newPosition; 
        }
        public void setHealth(int health)
        {
            this.health = health;
        }
    }
}
