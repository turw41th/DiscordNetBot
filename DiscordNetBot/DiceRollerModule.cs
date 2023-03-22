using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiscordNetBot
{
    internal class DiceRollerModule : ModuleBase<SocketCommandContext>
    {
        private static readonly Random random = new Random();
        private List<int> availableDice = new List<int>()
        {
            4, 6, 8, 10, 12, 20, 100
        };

        [SlashCommand("roll", "Rolls dice in the format XdY+Z. X ist the number of dice, Y is the number of sides on the die and Z is an additional modifier that can be positive or negative.")]
        public async Task RollAsync(SocketSlashCommand command)
        {
            string diceString = command.Data.Options.FirstOrDefault()?.Value.ToString();

            var regex = new Regex(@"^(\d+)d(\d+)([+-]\d+)?$");
            var match = regex.Match(diceString);

            if (!match.Success)
            {
                await command.RespondAsync("Invalid dice format! Use XdY+Z", ephemeral: true);
                return;
            }

            int numDice = int.Parse(match.Groups[1].Value);
            int numSides = int.Parse(match.Groups[2].Value);
            int modifier = match.Groups[3].Success ? int.Parse(match.Groups[3].Value) : 0;

            if (!availableDice.Contains(numSides))
            {
                await command.RespondAsync("Invalid number of sides! The available dice are d4, d6, d8, d10, d12, d20 and d100", ephemeral: true);
                return;
            }

            if (numDice < 1 || numDice > 100)
            {
                await command.RespondAsync("Invalid number of dice. You can roll between 1 and 100 dice.", ephemeral: true);
                return;
            }

            int total = 0;
            for (int i = 0; i < numDice; i++)
            {
                total += random.Next(1, numSides + 1);
            }

            total += modifier;

            string resultString = $"You rolled {numDice}d{numSides}";
            if (modifier > 0)
            {
                resultString += $"+{modifier}";
            }
            else if (modifier < 0)
            {
                resultString += $"{modifier}";
            }
            resultString += $": {total}";

            await command.RespondAsync(resultString, ephemeral: true);
        }
    }
}
