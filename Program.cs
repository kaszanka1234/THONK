using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace THONK{
    class Program{

        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands = new CommandService(new CommandServiceConfig{
            CaseSensitiveCommands = false,
            DefaultRunMode = RunMode.Async,
            LogLevel = LogSeverity.Debug
        });
        static void Main(String[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private Program(){
            _client = new DiscordSocketClient(new DiscordSocketConfig{
                LogLevel = LogSeverity.Debug,
                MessageCacheSize = 50
            });
        }
        private static Task Log(LogMessage msg){
            Console.WriteLine(msg);
            return Task.CompletedTask;
        }
        private async Task MainAsync(){
            _client.Log += Log;
            _client.MessageReceived += _client_MessageRecieved;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.Ready += _client_ready;
            String token = "NDkwNjIxMjExNTA0NTQxNzA3.DtRmDw.NChANeAMnz-4yy9tThm0zRwhR4w";
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            
            await Task.Delay(-1);
        }

        private async Task _client_ready(){
            await _client.SetGameAsync("Conquering the world", "");
        }

        private async Task _client_MessageRecieved(SocketMessage arg){
            throw new NotImplementedException();
        }
    }
}