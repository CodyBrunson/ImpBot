using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace ImpBot.Modules
{
    public class Destiny : ModuleBase<SocketCommandContext>
    {
        [Command("xur")]
        [Alias("x")]
        [Summary("Provides the location of Xur.")]
        public async Task Xur()
        {
            await ReplyAsync(XurLocation());
        }

        private string XurLocation()
        {
            return "Not yet implemented.";
        }
    }
}
