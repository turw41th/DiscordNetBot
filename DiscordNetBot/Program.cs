using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace DiscordNetBot
{
    class Program
    {
        public static Task Main(string[] args) => new Program().MainAsync();

        private DiscordSocketClient _client;
        private MessageHandler _messageHandler;

        public async Task MainAsync()
        {
            _messageHandler = new MessageHandler();
            _client = new DiscordSocketClient();

            _client.MessageReceived += _messageHandler.CommandHandler;

            _client.Log += Log;

            _client.Ready += Client_Ready;
            _client.SlashCommandExecuted += _messageHandler.SlashCommandHandler;
           
            var token = File.ReadAllText("Resources\\DiscordNetBot_token.txt");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        public async Task Client_Ready()
        {
            var globalRollCommand = new SlashCommandBuilder();
            globalRollCommand.WithName("roll");
            globalRollCommand.WithDescription("This is a dice rolling command!");
            globalRollCommand.AddOption("parameter", ApplicationCommandOptionType.String, "Format: XdY+Z", isRequired: true);

            try
            {
                await _client.CreateGlobalApplicationCommandAsync(globalRollCommand.Build());
            } catch (ApplicationCommandException ex)
            {
                var json = JsonConvert.SerializeObject(ex.Errors, Formatting.Indented);
                Console.WriteLine(json);
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}