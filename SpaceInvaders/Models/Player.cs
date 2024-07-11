namespace SpaceInvaders.Models
{
    public class Player : Entity
    {
        public string connectionId;
        public Player(string connectionId, int x, int y, int speed) : base(x, y, speed)
        {
            this.connectionId = connectionId;
        }
    }
}
