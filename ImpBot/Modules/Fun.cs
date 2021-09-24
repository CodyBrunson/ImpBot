using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace ImpBot.Modules
{
    public class Fun : ModuleBase<SocketCommandContext>
    {
        [Command("roll")]
        [Summary("Roll dice!")]
        public async Task Roll(String Dice)
        {
            if (!Dice.ToLower().Contains("d"))
            {
                await ReplyAsync("Incorrect syntax.  Example: 1d20");
                return;
            }
            List<String> Rolls = RollDice(Dice);
            StringBuilder DiceString = new("Rolls: ");
            foreach (var Roll in Rolls)
            {
                if (Roll == Rolls[^1])
                {
                    DiceString.Append("- Total: " + Roll);
                }
                else
                {
                    DiceString.Append(Roll + " ");
                }
            }
            await ReplyAsync(DiceString.ToString());
            
        }

        private List<String> RollDice(String Dice)
        {
            try
            {
                int _index = Dice.IndexOf("d");
                int AmountOfDice = Convert.ToInt32(Dice.Substring(0, _index));
                int TypeOfDice = Convert.ToInt32(Dice.Substring(_index + 1));
                int TotalRolled = 0;
                List<String> Rolls = new();

                for(int i = 0; i < AmountOfDice; i++)
                {
                    var Rolled = new Random().Next(1, TypeOfDice);
                    Rolls.Add(Rolled.ToString());
                    TotalRolled += Rolled;
                }
                Rolls.Add(TotalRolled.ToString());
                return Rolls;

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
