using Microsoft.AspNetCore.SignalR;
using Interfaces;
using System.ComponentModel;

namespace SpaceInvaders.Hubs
{

    // Class Implementation of player
    public class Player : IPlayer
    {
        public uint id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string connectionId = string.Empty;
        public string name;
        public string Name { get => name; set => value = name; }
        public int score { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int x_pos { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int y_pos { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Player(string connectionId, string name, int x = 0, int y = 0)
        {   
            this.connectionId = connectionId;
            this.name = name;
        }
        public void move_player()
        {
            throw new NotImplementedException();
        }
    }
    public class GameHub : Hub
    {
        public static List<Player> players = new List<Player>();  
        public static bool Exists(Player player)
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
        public async Task RegisterPlayer(string name)
        {
            string connectionId = Context.ConnectionId;
            Player player = new Player(connectionId, name);

            if(!Exists(player))
            {
                players.Add(player);
                Console.WriteLine($"Player Count: {players.Count}");
                foreach (Player p in players)
                {
                    Console.WriteLine($"{p.Name}, {p.connectionId}");
                }
                await Groups.AddToGroupAsync(connectionId, "Players");
                await Clients.Group("Players").SendAsync("PlayerConnected", connectionId);
            }
        }
        public async Task SendPlayerPosition(int x, int y)
        {
            string playerId = Context.ConnectionId;
            await Clients.All.SendAsync("RecievePlayerPosition", playerId, x, y);
        }
        // Joining a specific game room
        public async Task JoinGameRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("PlayerJoinedRoom", Context.ConnectionId);
        }
        // Leaving a specific game room
        public async Task LeaveGameRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("PlayerLeftRoom", Context.ConnectionId);
        }
    }
}