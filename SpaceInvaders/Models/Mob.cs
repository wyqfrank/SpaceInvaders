namespace SpaceInvaders.Models
{
    public class Mob : Entity
    {
        private bool canShoot; 
        public Mob(int[,] position, int health) : base(position, health)
        {
        }

    }
}
