using Microsoft.AspNetCore.SignalR;
using Interfaces;
using System.ComponentModel;
using Newtonsoft.Json;

namespace SpaceInvaders.Hubs
{
    // Class for encapsulating ..
    public class GameParty
    {   
        public List<Player> players = new List<Player>();
        public string partyId;
        public GameParty(string partyId)
        {
            this.partyId = partyId;
        }

        public override string ToString()
        {
            return $"Party id: {partyId}";
        }

    }
    public class Game
    {
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
        public string partyId;
        public string color = string.Empty;
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
            switch(direction)
            {
                case "left":
                    x_pos -= speed;
                    break;
                case "right":
                    x_pos += speed;
                    break;
                case "up":
                    y_pos -= speed;
                    break;
                case "down":
                    y_pos += speed;
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
            // Check if party exists
            if(game.PartyExists(currentPlayer.partyId))
            {
                // Move player
                currentPlayer.move_player(direction);

                // Send a serialised json containing the coordinates of the player
                await Clients.Group(currentPlayer.partyId).SendAsync("ReceivePlayerMove", JsonConvert.SerializeObject(game.GetPlayersInParty(currentPlayer.partyId)));
                // game.GetPlayersInParty(currentPlayer.partyId)
            }
        }
        // Joining a specific game room
        public async Task JoinGameRoom(string partyId)
        {   
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
                currentPlayer.partyId = partyId;
                game.groups[partyId].players.Add(currentPlayer);
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, partyId);
            await Clients.Group(partyId).SendAsync("PlayerJoinedRoom", Context.ConnectionId);
        }
        // Leaving a specific game room
        public async Task LeaveGameRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("PlayerLeftRoom", Context.ConnectionId);
        }
        public async Task RegisterUsername(string name)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("RecievedName", name);
        }
    }
}