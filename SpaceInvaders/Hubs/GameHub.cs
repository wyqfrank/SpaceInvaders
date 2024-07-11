using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System.Reactive.Linq;
using Newtonsoft.Json;
using SpaceInvaders.Models;

namespace SpaceInvaders.Hubs
{
    // Class for encapsulating .
    public sealed class HostedBroadcaster : IHostedService, IDisposable
    {
        private readonly IHubContext<GameHub> hubContext;
        private IDisposable subscription;

        public HostedBroadcaster(IHubContext<GameHub> hubContext)
        {
            this.hubContext = hubContext;
        }
        public void Dispose()
        {
            this.subscription?.Dispose();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.subscription = Observable.Interval(TimeSpan.FromMilliseconds(1000)).Subscribe(_ => hubContext.Clients.All.SendAsync("UpdateData"));
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.subscription?.Dispose();
            return Task.CompletedTask;
        }
    }
    public class Game
    {
        // HashMap for storing players for O(1) retrieval

        public readonly int Width = 720;
        public readonly int Height = 720;
        public Dictionary<string, Player> players = new Dictionary<string, Player>(); 
    }

    public class GameHub : Hub
    {
        // Have to make static cuz everytime client uses hub function class is reset,
        // use Dictionary to store key pair (connection id and player) as Dictionary has O(1) time complexity for inserting and fetching data
        public Game game;
        // Use singleton class as basis for storing data, use SQL for persistent data storag
        public GameHub(Game game)
        {
            this.game = game;
        }
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            game.players.Add(connectionId, new Player(connectionId, 0, 0, 10));
            await Clients.Caller.SendAsync("PlayerConnected", game.Width, game.Height);
            await UpdateData();
        }
        // Method for moving player given direction from user input
        public async Task UpdateData()
        {
            await Clients.All.SendAsync("RecieveData", JsonConvert.SerializeObject(game.players));
        }
        public async Task HandleInput(string input)
        {
            Player currentPlayer = game.players[Context.ConnectionId];
            currentPlayer.MovePlayer(input);
            await UpdateData();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            game.players.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}