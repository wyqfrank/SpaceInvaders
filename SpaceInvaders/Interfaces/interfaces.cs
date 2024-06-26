
using SpaceInvaders.Models;

namespace Interfaces;
public interface IPlayer{

    string Name { get; set; }
    int score { get; set; }
    int x_pos{ get; set; }
    int y_pos{ get; set; } 
    double speed {get; set; }
    void move_player();
}

public interface IBulletOBject{
    double speed { get; set; }
    int x_pos{ get; set; }
    int y_pos{ get; set; } 
}

public interface IAlien{

    int x_pos{ get; set; }
    int y_pos { get; set; }
    double speed { get; set; }
}

public interface IGame{

    List<IAlien> aliens{ get; set; }
    List<IPlayer> players {get; set;}

    void update_game();
}


public interface IBoard{
    int width { get; set; }
    int height { get; set; }

}

