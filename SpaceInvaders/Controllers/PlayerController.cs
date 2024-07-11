using SpaceInvaders.Models;
using System;

namespace SpaceInvaders.Controllers
{
    public class PlayerController
    {
        public void MovePlayer(Player player, string input)
        {

            int x = player.getX();
            int y = player.getY();
            int speed = player.getSpeed();

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
            player.setX(x);
            player.setY(y);
            Console.WriteLine(x+ ","+y); 
            // check if can be done using tuples
        }
    }
}
