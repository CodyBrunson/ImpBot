using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpBot.Modules
{
    public class Utility : ModuleBase<SocketCommandContext>
    {

        [Command("ping")]
        [Summary("Show current latency.")]
        public async Task Ping()
        {
            Console.WriteLine("Ping requested by: " + Context.Client);
            await ReplyAsync($"Latency: " + Context.Client.Latency + "ms");
        }
    }
}
