using System;
using System.Net;
using System.Threading.Tasks;
using Discord.Commands;
using HtmlAgilityPack;

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

        private static string XurLocation()
        {
            using (WebClient _WC = new())
            {
                String HTML = _WC.DownloadString("https://whereisxur.com/");
                HtmlDocument Test = new();
                Test.LoadHtml(HTML);
                HtmlNodeCollection collection = Test.DocumentNode.SelectNodes("//h4");
                foreach(HtmlNode title in collection)
                {
                    if(title.InnerText.Contains("Xûr"))
                        return title.InnerText.ToString();
                }
            }
            return "Check back later...";
        }
    }
}
