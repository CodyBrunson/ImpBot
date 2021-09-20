using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Discord;
using System.Linq;
using Newtonsoft.Json;
using ImpBot.Tools;

namespace ImpBot
{
    public class CommandHandler
    {

        private readonly CommandService Commands;
        private readonly DiscordSocketClient Client;
        private readonly IServiceProvider Services;

        public CommandHandler(IServiceProvider Services)
        {
            this.Commands = Services.GetRequiredService<CommandService>();
            this.Client = Services.GetRequiredService<DiscordSocketClient>();
            this.Services = Services;

            Client.Ready += ClientReadyAsync;
            Client.MessageReceived += HandleCommandAsync;

        }

        private async Task HandleCommandAsync(SocketMessage Message)
        {
            if (Message.Author.IsBot || !(Message is SocketUserMessage _message) || _message.Channel is IDMChannel)
                return;

            var Context = new SocketCommandContext(Client, _message);

            int argPos = 0;

            JObject Config = Functions.GetConfig();
            string CommandPrefix = Config["Prefix"].Value<String>();
            if (_message.HasStringPrefix(CommandPrefix, ref argPos) || _message.HasMentionPrefix(Client.CurrentUser, ref argPos))
            {
                var Result = await Commands.ExecuteAsync(Context, argPos, Services);

                if (!Result.IsSuccess && Result.Error.HasValue)
                    await Context.Channel.SendMessageAsync($":x: {Result.ErrorReason}");
            }
        }

        private async Task ClientReadyAsync()
            => await Functions.SetBotStatusAsync(Client);

        public async Task InitializeAsync()
            => await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), Services);
    }
}
