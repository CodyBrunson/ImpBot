using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ImpBot.Tools;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace ImpBot
{
    public class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            using var Services = ConfigureServices();

            Console.WriteLine("Starting ImpBot...");
            var Client = Services.GetRequiredService<DiscordSocketClient>();

            JObject Config = Functions.GetConfig();
            String Token = Config["Token"].Value<string>();

            await Client.LoginAsync(TokenType.Bot, Token);
            await Client.StartAsync();

            await Services.GetRequiredService<CommandHandler>().InitializeAsync();

            await Task.Delay(-1);
        }
        public ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                {
                    MessageCacheSize = 500,
                    LogLevel = LogSeverity.Info
                }))
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    LogLevel = LogSeverity.Info,
                    DefaultRunMode = RunMode.Async,
                    CaseSensitiveCommands = false
                }))
                .AddSingleton<CommandHandler>()
                .BuildServiceProvider();
        }

        private Task Log(LogMessage Log)
        {
            Console.WriteLine(Log.ToString());
            return Task.CompletedTask;
        }
    }
}
