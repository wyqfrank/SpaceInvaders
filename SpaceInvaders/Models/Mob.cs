namespace SpaceInvaders.Models
{
    public class Mob : Entity
    {
        private bool canShoot; 
        private MobType type;
        public Mob(int[,] position, int health, MobType type) : base(position, health)
        {
            this.type = type;
        }
        
    }
}
