using Microsoft.AspNetCore.SignalR;
using Interfaces;
using System.ComponentModel;

namespace SpaceInvaders.Hubs
{

    // Class Implementation of player
    public class Game{
        public List<Player> players = new List<Player>();
        public string partyId = string.Empty;
        public Game(string partyId)
        {
            this.partyId = partyId;
        }
        public void PrintPlayers()
        {
            foreach (var player in players)
            {
                Console.WriteLine(player.name);
            }
        }
        public bool Exists(Player player)
        {
            foreach(var p in players)
            {
                if(p.connectionId == player.connectionId)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class Player
    {
        public string partyId = null;
        public string connectionId = string.Empty;
        public string name;
        public int score;
        public int x_pos;
        public int y_pos;
        public int speed = 10;
        public Player(string connectionId, string name = "default", int x = 310, int y = -310)
        {
            this.connectionId = connectionId;
            this.name = name;
        }
        public void move_player(string direction)
        {
            if(direction == "left")
            {
                x_pos -= speed;
            }
            else if(direction == "right")
            {
                x_pos += speed;
            }
        }
    }
    public class GameHub : Hub
    {
        public static List<Game> Games = new List<Game>();
        public Player ? currentPlayer = null;
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            Player player = new Player(connectionId);
            currentPlayer = player;
            await Groups.AddToGroupAsync(connectionId, "Players");
            await Clients.Group("Players").SendAsync("PlayerConnected", connectionId);
        }
        public async Task MovePlayer(string direction)
        {
            Console.WriteLine(direction);
            if(currentPlayer != null && currentPlayer.partyId != null)
            {
                Console.WriteLine("MOving ");
                currentPlayer.move_player(direction);

                // use group to make private server/party for users
                await Clients.Group(currentPlayer.partyId).SendAsync("ReceivePlayerMove", currentPlayer.x_pos, currentPlayer.y_pos);
            }
        }
        // Joining a specific game room
        public async Task JoinGameRoom(string partyId)
        {
            if(currentPlayer != null)
            {
                currentPlayer.partyId = partyId;
                await Groups.AddToGroupAsync(Context.ConnectionId, partyId);
                await Clients.Group(partyId).SendAsync("PlayerJoinedRoom", Context.ConnectionId);
            }
        }
        // Leaving a specific game room
        public async Task LeaveGameRoom(string roomId)
        {
            if(currentPlayer != null)
            {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("PlayerLeftRoom", Context.ConnectionId);
            }
        }
    }
}