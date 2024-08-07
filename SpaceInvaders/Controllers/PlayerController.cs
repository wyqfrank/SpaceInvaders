using SpaceInvaders.Models;
using System;

namespace SpaceInvaders.Controllers
{
    public class PlayerController
    {

        private int mapHeight;
        private int mapWidth;
        public PlayerController(int mapHeight, int mapWidth)
        {
            this.mapHeight = mapHeight;
            this.mapWidth = mapWidth;
        }

        public List<Bullet> GetBulletData(List<Player> players)
        {
            List<Bullet> bullets = new List<Bullet>();
            foreach(var player in players)
            {
                foreach(var bullet in player.bullets)
                {
                    bullet.Update();
                }
                bullets.AddRange(player.bullets);
            }
            return bullets;
        }

        // public void PlayerShoot(Player player)
        // {
        //     Bullet bullet = new Bullet(player.getX(), player.getY(), Guid.NewGuid().ToString());
        //     player.bullets.Add(bullet);
        // }
        public void MovePlayer(Player player, string input)
        {
            int x = player.getX();
            int y = player.getY();
            int speed = player.getSpeed();
            // confused
            switch (input)
            {
                case "wd":
                    y -= speed;
                    x += speed;
                    break;
                case "sd":
                    y += speed;
                    x += speed;
                    break;
                case "wa":
                    y -= speed;
                    x -= speed;
                    break;
                case "sa":
                    y += speed;
                    x -= speed;
                    break;
                case "w":
                    y -= speed;
                    break;
                case "s":
                    y += speed;
                    break;
                case "a":
                    x -= speed;
                    break;
                case "d":
                    x += speed;
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    return;
            }
            if(x <= 0 || x >= mapWidth || y <= 0 || y >= mapHeight)
            {
                Console.WriteLine("Out of border");
            }
            else
            {
                player.setX(x);
                player.setY(y);
            }
            Console.WriteLine(x+ ","+y); 
            // check if can be done using tuples
        }


        
    }
}
