using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpBot.Modules
{
    public class Admin : ModuleBase<SocketCommandContext>
    {

        [Command("kick")]
        [Summary("Kick a usr from the server.")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick(SocketGuildUser Target, [Remainder] string Reason = "No reason provided.")
        {
            await Target.KickAsync(Reason);
            await ReplyAsync($"**{Target}** has been kicked. Bye Bye :wave:");
        }
    }
}
