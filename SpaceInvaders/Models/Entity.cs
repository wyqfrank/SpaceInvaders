namespace SpaceInvaders.Models
{
    public abstract class Entity
    {
        public int x;
        public int y;
        public int health;
        public int speed;

        public Entity(int x, int y, int speed = 10, int health = 100)
        {
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.health = health;
        }
        // change getter and setters to C# standard
        //public int X 
        //{
        //    get { return X; }
        //    set { x = value; }

        //}

        //public int Y
        //{
        //    get { return Y; }
        //    set { y = value; }

        //}

        public int getX()
        {
            return this.x;
        }

        public void setX(int newX)
        {
            this.x = newX;
        }

        public int getY()
        {
            return this.y;
        }

        public void setY(int newY)
        {
            this.y = newY;
        }

        public int getHealth()
        {
            return this.health;
        }

        public void setHealth(int health)
        {
            this.health = health;
        }

        public int getSpeed()
        {
            return this.speed;
        }

        public void setSpeed(int speed)
        {
            this.speed = speed;
        }
    }
}
