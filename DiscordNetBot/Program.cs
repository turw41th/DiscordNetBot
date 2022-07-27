using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace DiscordNetBot
{
    class Program
    {
        public static Task Main(string[] args) => new Program().MainAsync();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.MessageReceived += new MessageHandler().CommandHandler;

            _client.Log += Log;
           
            var token = File.ReadAllText("Resources\\DiscordNetBot_token.txt");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}