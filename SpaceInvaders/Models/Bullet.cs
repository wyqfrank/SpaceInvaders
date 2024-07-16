namespace SpaceInvaders.Models
{
    public class Bullet
    {
        public int speed = 30;
        public int x;
        public int y;
        public bool used = false;
        public Bullet(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        public void Update()
        {
            y-= speed;
        }
    }
}