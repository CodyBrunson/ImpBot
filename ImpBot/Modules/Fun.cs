using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace ImpBot.Modules
{
    public class Fun : ModuleBase<SocketCommandContext>
    {
        //This code is really bad and could be improved by using try catch and exceptions but didn't want to write it that way for now.  #Lazy?
        //TODO: Re-write this code using try catch.
        [Command("roll")]
        [Summary("Roll dice!")]
        public async Task Roll(String Dice)
        {
            if (!Dice.ToLower().Contains("d"))
            {
                await ReplyAsync("Incorrect syntax.  Example: 1d20");
                return;
            }
            else
            {
                List<String> Rolls = RollDice(Dice);
                StringBuilder DiceString = new("Rolls: ");
                if (Rolls[0].Contains("dick"))
                {
                    await ReplyAsync("Don't be a dick...");
                    return;
                }
                else
                {
                    if (Rolls.Count != 2)
                    {
                        foreach (var Roll in Rolls)
                        {
                            if (Roll != Rolls[^1])
                            {
                                DiceString.Append(Roll + " ");
                            }
                        }
                    }
                    else
                    {
                        DiceString.Append(Rolls[0] + " ");
                    }
                    DiceString.Append("- Total: " + Rolls[^1]);
                    await ReplyAsync(DiceString.ToString());
                }
            }
            
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
                if(AmountOfDice > 100)
                {
                    Rolls.Add("Don't be a dick...");
                    Rolls.Add(" ");
                    return Rolls;
                }

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
