using Microsoft.AspNetCore.SignalR;
using Interfaces;
using System.ComponentModel;
using Newtonsoft.Json;
using SpaceInvaders.Models;

namespace SpaceInvaders.Hubs
{
    // Class for encapsulating ..
    
    public class Bullet
    {
        public int height = 16;
        public int width = 2;
        public bool used = false;
        public int x;
        public int y;
        public int speed = 50;
        public Bullet(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void shoot()
        {
            while(y <= 0)
            {
                y-= speed;
            }
        }
    }
    public class GameParty
    {   
        public List<Player> players = new List<Player>();
        public string partyId;
        public GameParty(string partyId)
        {
            this.partyId = partyId;
        }
        public bool playerExists(string connectionId)
        {
            foreach(Player p in players)
            {
                if(p.connectionId == connectionId)
                {
                    return true;
                }
            }
            return false;
        }
        public override string ToString()
        {
            return $"Party id: {partyId}";
        }
    }
    public class Game
    {
        // HashMap for storing players for O(1) retrieval
        public Dictionary<string, Player> players = new Dictionary<string, Player>(); 
        public Dictionary<string, GameParty> groups = new Dictionary<string, GameParty>();
        public void MovePlayer(string connectionId, string direction)
        {
            players[connectionId].move_player(direction);
        }
        public void PrintInfo()
        {
            foreach (var player in players.Values)
            {
                Console.WriteLine(player.connectionId);
            }
        }
        public Player FindPlayer(string connectionId)
        {
            return players[connectionId];
        }
        public bool PartyExists(string partyId)
        {
            return groups.ContainsKey(partyId);
        }
        public List<Player> GetAllPlayerData()
        {
            return players.Values.ToList();
        }
        public List<Player> GetPlayersInParty(string partyId)
        {
            return groups[partyId].players;
        }
    }
    public class Player
    {
        public List<Bullet> bullets = new();
        public int BoardWidth = 720;
        public int BoardHeight = 720;
        public string ? partyId = null;
        public string color = string.Empty;
        public string connectionId = string.Empty;
        public string name;
        public int score;
        public int x;
        public int y;
        public int speed = 10;
        public Player(string connectionId, string name = "default", int x = 320, int y = 320)
        {
            this.connectionId = connectionId;
            this.name = name;
            this.x = x;
            this.y = y;
        }

        public void shoot()
        {
            Bullet bullet = new Bullet(x, y);
            bullets.Add(bullet);
        }
        public void move_player(string direction)
        {
            switch(direction)
            {
                case "left":

                    x -= speed;
                    break;
                case "right":
                    x += speed;
                    break;
                case "up":
                    y -= speed;
                    break;
                case "down":
                    y += speed;
                    break;
                default:
                    break;
            }
        }
    }
    public interface IGameClient{
        Task PlayerJoinedRoom(string playerId);
    }
    public class GameHub : Hub
    {
        // Have to make static cuz everytime client uses hub function class is reset,
        // use Dictionary to store key pair (connection id and player) as Dictionary has O(1) time complexity for inserting and fetching data
        public Game game;
        // Use singleton class as basis for storing data, use SQL for persistent data storage

        public static GameMap gameMap = new (50,50);

        public GameHub(Game game)
        {
            this.game = game;

        }
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            // Add the player the storage
            game.players.Add(connectionId, new Player(connectionId));
            await Groups.AddToGroupAsync(connectionId, "Players");
            await Clients.Group("Players").SendAsync("PlayerConnected", connectionId);
        }
        // Method for moving player given direction from user input
        public async Task MovePlayer(string direction)
        {
            // Get the current player by accessing the dictionary
            Player currentPlayer = game.FindPlayer(Context.ConnectionId);
            if(string.IsNullOrEmpty(currentPlayer.partyId))
            {
                await SendErrorMessage("Party id cannot be empty");
                return;
            }
            // Check if party exists
            if(game.PartyExists(currentPlayer.partyId))
            {
                // Move player
                currentPlayer.move_player(direction);
                // Send a serialised json containing the coordinates of the player
                await SendData(currentPlayer.partyId);
                // game.GetPlayersInParty(currentPlayer.partyId)
            }
        }
        // public async Task UpdateBullets()
        // {
        //     Player currentPlayer = game.FindPlayer(Context.ConnectionId);

        //     if(currentPlayer.bullets.Count != 0)
        //     {
        //         foreach(var bullet in currentPlayer.bullets)
        //         {

        //         }
        //     }
        //     await Clients.Group(currentPlayer.partyId).SendAsync("ReceivePlayerMove", JsonConvert.SerializeObject(game.GetPlayersInParty(currentPlayer.partyId)));
        // }

        // Joining a specific game room
        public async Task JoinGameRoom(string partyId)
        {   
            if(string.IsNullOrEmpty(partyId))
            {
                await SendErrorMessage("Party id cannot be empty");
                return;
            }
            Player currentPlayer = game.FindPlayer(Context.ConnectionId);
            if(!game.PartyExists(partyId))
            {
                GameParty gameGroup = new GameParty(partyId); 
                currentPlayer.partyId = partyId;
                gameGroup.players.Add(currentPlayer); 
                game.groups.Add(partyId, gameGroup);
            }
            else
            {
                if(!game.groups[partyId].playerExists(currentPlayer.connectionId))
                {
                    currentPlayer.partyId = partyId;
                    game.groups[partyId].players.Add(currentPlayer);
                }
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, partyId);
            await Clients.Group(partyId).SendAsync("PlayerJoinedRoom", Context.ConnectionId);
            await SendData(partyId);
        }

        public async Task SendData(string partyId)
        {
            await Clients.Group(partyId).SendAsync("RecieveData", JsonConvert.SerializeObject(game.GetPlayersInParty(partyId)));
        }
        // Method for handling error
        public async Task SendErrorMessage(string message)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("RecieveError", message);
        }
        // Leaving a specific game room
        public async Task LeaveGameRoom()
        {

            Player currentPlayer = game.FindPlayer(Context.ConnectionId);

            if(currentPlayer.partyId != null)
            {
                game.groups[currentPlayer.partyId].players.Remove(currentPlayer);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, currentPlayer.partyId);
                await Clients.Group(currentPlayer.partyId).SendAsync("PlayerLeftParty", Context.ConnectionId);
            }
            else
            {
                string message = $"Player with id: {currentPlayer.connectionId} has not joined a room";
                await SendErrorMessage(message);
            }
        }
        public async Task RegisterUser(string name, string colour)
        {
            Player currentPlayer = game.FindPlayer(Context.ConnectionId);
            currentPlayer.color = colour;
            currentPlayer.name = name;
            await Clients.Client(Context.ConnectionId).SendAsync("RecievedName", name);
        }

        public async Task SendMap()
        {
            await Clients.All.SendAsync("ReceiveMapData",JsonConvert.SerializeObject(gameMap.GetMapLayout()));
        }
    }
}