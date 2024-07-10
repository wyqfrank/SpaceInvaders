namespace SpaceInvaders.Models2;
public static class Mappers
    {
        public static PlayerDto ToPlayerDto(this Player player)
        {
            return new PlayerDto
            {
                ConnectionId = player.connectionId,
                X = player.X,
                Y = player.Y
            };
        }
    }


public class PlayerDto
{
    public string ConnectionId { get; set; } = string.Empty;
    public int X { get; set; }
    public int Y { get; set; }
}