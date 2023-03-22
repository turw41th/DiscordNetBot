using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiscordNetBot
{
    internal class CommandMethods
    {
        private static readonly Random random = new Random();
        private static List<int> availableDice = new List<int>()
        {
            4, 6, 8, 10, 12, 20, 100
        };

        public static string HelloCommand()
        {
            return "World!";
        }


        public static string RollCommand(string command)
        {
            string diceString = command;

            var regex = new Regex(@"^(\d+)d(\d+)([+-]\d+)?$");
            var match = regex.Match(diceString);

            if (!match.Success)
            {
                return "Invalid dice format! Use XdY+Z";
            }

            int numDice = int.Parse(match.Groups[1].Value);
            int numSides = int.Parse(match.Groups[2].Value);
            int modifier = match.Groups[3].Success ? int.Parse(match.Groups[3].Value) : 0;

            if (!availableDice.Contains(numSides))
            {
                return "Invalid number of sides! The available dice are d4, d6, d8, d10, d12, d20 and d100";
            }

            int total = 0;
            for (int i = 0; i < numDice; i++)
            {
                total += random.Next(1, numSides + 1);
            }

            total += modifier;

            string result = $"You rolled {numDice}d{numSides}";
            if (modifier > 0)
            {
                result += $"+{modifier}";
            }
            else if (modifier < 0)
            {
                result += $"{modifier}";
            }
            result += $": {total}";

            return result;
        }

    }
}
