using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpBot.Tools
{
    public static class Functions
    {

        public static async Task SetBotStatusAsync(DiscordSocketClient Client)
        {
            JObject Config = GetConfig();

            String Currently = Config["Currently"]?.Value<String>();
            String PlayStatus = Config["PlayStatus"]?.Value<String>();
            String OnlineStatus = Config["OnlineStatus"]?.Value<String>();

            if(!String.IsNullOrEmpty(OnlineStatus))
            {
                UserStatus _userStatus = OnlineStatus switch
                {
                    "DND" => UserStatus.DoNotDisturb,
                    "Idle" => UserStatus.Idle,
                    "Offline" => UserStatus.Invisible,
                    _ => UserStatus.Online
                };

                await Client.SetStatusAsync(_userStatus);
                Console.WriteLine($"{DateTime.Now.ToString("hh:mm:ss")} | Online status set to {OnlineStatus}");
            }


            if (!String.IsNullOrEmpty(Currently) && !String.IsNullOrEmpty(PlayStatus))
            {
                ActivityType Activity = Currently switch
                {
                    "Listening" => ActivityType.Listening,
                    "Watching" => ActivityType.Watching,
                    "Streaming" => ActivityType.Streaming,
                    _ => ActivityType.Playing
                };

                await Client.SetGameAsync(PlayStatus, type: Activity);
                Console.WriteLine($"{DateTime.Now.ToString("hh:mm:ss")} | Playing status set | {Activity}: {PlayStatus}");
            }
        }


        public static JObject GetConfig()
        {
            using StreamReader ConfigJson = new StreamReader(Directory.GetCurrentDirectory() + @"/Config.json");
                return (JObject)JsonConvert.DeserializeObject(ConfigJson.ReadToEnd());
        }

    }
}
