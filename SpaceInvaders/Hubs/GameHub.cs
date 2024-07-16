using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using SpaceInvaders.Models;
using SpaceInvaders.Controllers;
using Newtonsoft.Json;

namespace SpaceInvaders.Hubs
{
    public sealed class HostedBroadcaster : IHostedService, IDisposable
    {
        private readonly IHubContext<GameHub> hubContext;
        private readonly Game game;
        private IDisposable subscription;

        public HostedBroadcaster(IHubContext<GameHub> hubcontext, Game game)
        {
            this.hubContext = hubcontext;
            this.game = game;
        }
        public void Dispose()
        {
            this.subscription?.Dispose();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            List<Mob> mobPattern = game.mobPatternGenerator.GenerateRandomMobPattern(game.Width, game.Height, 5);

            this.subscription = Observable.Interval(TimeSpan.FromMilliseconds(1000)).Subscribe(_ => 
                hubContext.Clients.All.SendAsync("GlobalUpdate", JsonConvert.SerializeObject(game.mobPatternGenerator.MoveMob(mobPattern)), JsonConvert.SerializeObject(game.GetBulletData()))
                );
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.subscription?.Dispose();
            return Task.CompletedTask;
        }
    }
}
    public class Game
    {
        // HashMap for storing players for O(1) retrieval

        public readonly int Width = 500;
        public readonly int Height = 500;
        public Dictionary<string, Player> players = new Dictionary<string, Player>();
        public PlayerController playerController = new PlayerController(500, 500);
        public Dictionary<string, Mob> mobs = new Dictionary<string, Mob>();
        public MobPatternGenerator mobPatternGenerator = new MobPatternGenerator();
        public List<Bullet> GetBulletData()
        {
            List<Bullet> bullets = new List<Bullet>();
            foreach (Player player in players.Values.ToList())
            {
                bullets.AddRange(UpdateBullets(player, mobs));
            }
            return bullets;
        }

        public List<Bullet> UpdateBullets(Player player, Dictionary<string, Mob> mobs)
        {
            List<Bullet> bulletsToRemove = new List<Bullet>();
            List<Bullet> updatedBullets = new List<Bullet>();
            Console.WriteLine(player.bullets.Count);
            foreach (Bullet bullet in player.bullets.ToList())
                {
                Console.WriteLine(bullet.ToString());
                bullet.Update();

                if (bullet.y <= 0)
                {
                   
                    bullet.Used = true;
                    bulletsToRemove.Add(bullet);
                }
                foreach (Mob mob in mobs.Values)
                {
                    bullet.DoDamage(bullet.x, bullet.y, 5, mob);
                    if (bullet.Used)
                    {
                        bulletsToRemove.Add(bullet);
                        break; // Exit the loop as the bullet is now used
                    }
                }

                if (!bullet.Used)
                {
                    updatedBullets.Add(bullet);
                }
            }

            // Remove bullets that were marked as Used
            foreach (var bullet in bulletsToRemove)
            {
                player.bullets.Remove(bullet);
            }
            Console.WriteLine(player.bullets.Count);
            return updatedBullets;
        }


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

            Console.WriteLine(input);
            Player currentPlayer = game.players[Context.ConnectionId];
            game.playerController.MovePlayer(currentPlayer, input);
            
            await UpdateData();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            game.players.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
        public void Shoot()
        {
            Player currentPlayer = game.players[Context.ConnectionId];
            currentPlayer.Shoot();
        }
        // public async Task SendMobData()
        // {
        //     await Clients.All.SendAsync("RecieveMobData", JsonConvert.SerializeObject(game.mobs));
        // }

        // public async Task sendMobPattern()
        // {
        //     List<Mob> mobPattern = game.mobPatternGenerator.GenerateRandomMobPattern(game.Width, game.Height, 5);
        //     await Clients.All.SendAsync("RecieveMobData", JsonConvert.SerializeObject(mobPattern));
        // }
    }