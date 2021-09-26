using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace ImpBot.Modules
{
    public class Fun : ModuleBase<SocketCommandContext>
    {
        //Rolls dice in syntax #d##.  Example 1d20.
        [Command("roll")]
        [Alias("r")]
        [Summary("Roll dice!")]
        public async Task Roll(String Dice)
        {
            try
            {
                await ReplyAsync(RollDice(Dice));
            }
            catch (Exception ex) { await ReplyAsync("Unknown Error: " + ex.Message); }

        }

        private static String RollDice(String Dice)
        {
            if (!Dice.ToLower().Contains("d")) return "Invalid roll syntax. Example: /r 1d20";

            int _index = Dice.ToLower().IndexOf("d");
            int DiceToRoll = Convert.ToInt32(Dice.Substring(_index + 1));

            if (_index == 0) return "Roll: " + new Random().Next(1, DiceToRoll).ToString();

            int AmountOfDice = Convert.ToInt32(Dice.Substring(0, _index));

            if (AmountOfDice > 100) return "Don't be a dick...";
            if(AmountOfDice == 1) return "Roll: " + new Random().Next(1, DiceToRoll).ToString();

            StringBuilder RollString = new("Rolls: ");
            int DiceTotal = 0;
            for(int Roll = 1; Roll <= AmountOfDice; Roll += 1)
            {
                int Rolled = new Random().Next(1, DiceToRoll);
                RollString.Append(Rolled.ToString() + " ");
                DiceTotal += Rolled;
            }
            RollString.Append("- Total: " + DiceTotal);
            return RollString.ToString();
        }

        [Command("raid")]
        [Summary("RAID? RAID? RAID? RAID?")]
        public async Task Raid()
        {
            await ReplyAsync("Did someone say raid?");
            await ReplyAsync($"https://tenor.com/view/raid-destiny-seagull-gif-19244751");
        }
    }
}
