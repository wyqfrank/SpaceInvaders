namespace SpaceInvaders.Models;

public class Bullet
{
    public string id;
    public int speed = 30;
    public int x;
    public int y;
    public bool Used = false;
    public Bullet(int x, int y, string id)
    {
        this.id = id;
        this.x = x;
        this.y = y;
    }

    public void Update()
    {
        y -= speed;
    }
}