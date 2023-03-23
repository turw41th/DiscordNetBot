using Discord.WebSocket;

namespace DiscordNetBot
{
    public class MessageHandler
    {

        public Task CommandHandler(SocketMessage message)
        {
            string messageText = message.Content.ToString();
            string messageAuthor = message.Author.ToString();

            if (!messageText.StartsWith("!"))
            {
                return Task.CompletedTask;
            }

            if (message.Author.IsBot)
            {
                return Task.CompletedTask;
            }

            if (messageText.Equals("!hello"))
            {
                message.Channel.SendMessageAsync(CommandMethods.HelloCommand());
            }

            if (messageText.StartsWith("!roll "))
            {
                string commandString = messageText.Substring(messageText.IndexOf("!roll ") + 6);
                message.Channel.SendMessageAsync(CommandMethods.RollCommand(commandString));
            }

            return Task.CompletedTask;
        }

        public async Task SlashCommandHandler(SocketSlashCommand command)
        {
            switch (command.CommandName)
            {
                case "roll":
                    var message = command.Data.Options.First().Value as string;

                    await command.RespondAsync(CommandMethods.RollCommand(message));
                    break;
            }

            
        }

    }
}
