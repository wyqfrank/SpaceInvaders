using System.Security.Cryptography.X509Certificates;

namespace SpaceInvaders.Models
{
    public class Player : Entity
    {
        private int score = 0;
        public Player(int[][] position, int health) : base(position, health)
        {

        }

        public int getScore()
        {
            return score;
        }

        public void incrementScore(int amount)
        {
            score += amount;
        }

    }
}
