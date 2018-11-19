using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace THONK{
    class Program{

        private static String _token;
        private readonly DiscordSocketClient _client;
        private readonly IServiceCollection _map = new ServiceCollection();
        private IServiceProvider _services;
        private readonly CommandService _commands = new CommandService(new CommandServiceConfig{
            CaseSensitiveCommands = false,
            DefaultRunMode = RunMode.Async,
            LogLevel = LogSeverity.Verbose
        });
        static void Main(String[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private Program(){
            _client = new DiscordSocketClient(new DiscordSocketConfig{
                LogLevel = LogSeverity.Verbose,
                MessageCacheSize = 50
            });
        }
        private static Task Log(LogMessage msg){
            var cc = Console.ForegroundColor;
            switch (msg.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.WriteLine($"{DateTime.Now,-19} [{msg.Severity,8}] {msg.Source}: {msg.Message}");
            Console.ForegroundColor = cc;
            return Task.CompletedTask;
        }
        private async Task MainAsync(){
            string jsonString = File.ReadAllText("./config.json");
            dynamic json = JsonConvert.DeserializeObject(jsonString);
            _token = json.token;
            _client.Log += Log;
            _client.Ready += _client_ready;
            await InitCommands();
            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
            
            await Task.Delay(-1);
        }

        private async Task InitCommands(){
            _services = _map.BuildServiceProvider();
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.MessageReceived += _client_MessageRecieved;
        }

        private async Task _client_ready(){
            await _client.SetGameAsync("with async functions", "");
        }

        private async Task _client_MessageRecieved(SocketMessage MessageParam){
            var Message = MessageParam as SocketUserMessage;
            if(Message == null) return;
            SocketCommandContext Context = new SocketCommandContext(_client, Message);
            if(Context.User.IsBot) return;
            int ArgPos = 0;
            String Prefix = "?dev";
            if(!Message.HasStringPrefix(Prefix, ref ArgPos))return;
            var Result = await _commands.ExecuteAsync(Context, ArgPos);
            if(!Result.IsSuccess){
                await Log(new LogMessage(LogSeverity.Error, "cmd hndl", "Cannot execute command"));
            }
        }
    }
}