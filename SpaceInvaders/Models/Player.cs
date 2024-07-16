namespace SpaceInvaders.Models
{
    public class Player : Entity
    {
        public Dictionary<string, Bullet> bullets = new();
        public string connectionId;
        public Player(string connectionId, int x, int y, int speed) : base(x, y, speed)
        {
            this.connectionId = connectionId;
        }
        public void Shoot()
        {
            Bullet bullet = new Bullet(x, y);
            bullets.Add(Guid.NewGuid().ToString(), bullet);
        }
    }
}
