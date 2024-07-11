namespace SpaceInvaders.Models
{
    public class Player : Entity
    {
        public string connectionId;
        public Player(string connectionId, int x, int y, int speed) : base(x, y, speed)
        {
            this.connectionId = connectionId;
        }
        public void MovePlayer(string input)
        {
            switch(input)
            {
                case "wd":
                    y-=speed;
                    x+=speed;
                    break;
                case "sd":
                    y+=speed;
                    x+=speed;
                    break;
                case "wa":
                    y-=speed;
                    x-=speed;
                    break;
                case "sa":
                    y+=speed;
                    x-=speed;
                    break;
                case "w":
                    y-=speed;
                    break;
                case "s":
                    y+=speed;
                    break;
                case "a":
                    x-=speed;
                    break;
                case "d":
                    x+=speed;
                    break;
            }
        }

    }
}
