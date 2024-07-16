namespace SpaceInvaders.Models
{
    public class Player : Entity
    {
        public List<Bullet> bullets = new();
        public string connectionId;
        public Player(string connectionId, int x, int y, int speed) : base(x, y, speed)
        {
            this.connectionId = connectionId;
        }
        public void Shoot()
        {
            Bullet bullet = new Bullet(x, y, Guid.NewGuid().ToString());
            bullets.Add(bullet);
        }
    }
}
