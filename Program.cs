using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace THONK{
    class Program{

        /* define client var */
        private readonly DiscordSocketClient _client;
        /* define services map */
        private readonly IServiceCollection _map = new ServiceCollection();
        /* define service provider */
        private IServiceProvider _services;
        /* initialize command service */
        private readonly CommandService _commands = new CommandService(new CommandServiceConfig{
            CaseSensitiveCommands = false,
            DefaultRunMode = RunMode.Async,
            LogLevel = LogSeverity.Verbose
        });
        /* run main asynchronous function */
        static void Main(String[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        /* assign values to read only vars */
        private Program(){
            /* define client */
            _client = new DiscordSocketClient(new DiscordSocketConfig{
                /* set logging level (Critical, Error, Warning, Info, Verbose, Debug) */
                LogLevel = LogSeverity.Verbose,
                MessageCacheSize = 50
            });
        }
        /* logging method
         * LogMessage (Severity :LogSeverity:, Source :string:, Message :string:) */
        public static Task Log(LogMessage msg){
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
        /* main program method */
        private async Task MainAsync(){
            
            /* define token var */
            String token = Environment.GetEnvironmentVariable("TOKEN");
            /* subscribe client to logging service */
            _client.Log += Log;
            /* set rich presence properties */
            _client.Ready += _client_ready;
            /* initialize commands and login as bot */
            await InitCommands();
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            
            /* wait indefinetly so the bot program won't finish */
            await Task.Delay(-1);
        }

        /* commands initialization method */
        private async Task InitCommands(){
            /* build services */
            _services = _map.BuildServiceProvider();
            /* load all modules from subdirectories (all modules must be public) */
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
            /* execute method after recieving message */
            _client.MessageReceived += _client_MessageRecieved;
        }

        /* set rich presence properties */
        private async Task _client_ready(){
            await _client.SetGameAsync("with async functions", "");
        }

        /* method executed at recieveing message */
        private async Task _client_MessageRecieved(SocketMessage MessageParam){
            /* define Message var */
            var Message = MessageParam as SocketUserMessage;
            /* return if message is empty */
            if(Message == null) return;
            /* set new command context */
            SocketCommandContext Context = new SocketCommandContext(_client, Message);
            /* don't allow bots to execute commands */
            if(Context.User.IsBot) return;
            /* position of command prefix */
            int ArgPos = 0;
            /* command prefix */
            String Prefix = "?dev";
            /* return if message doesn't have a prefix */
            if(!Message.HasStringPrefix(Prefix, ref ArgPos))return;
            /* get the result of executed command */
            var Result = await _commands.ExecuteAsync(Context, ArgPos);
            /* log to console if command failed */
            if(!Result.IsSuccess){
                await Log(new LogMessage(LogSeverity.Error, "cmd handler", Result.ErrorReason));
            }
        }
    }
}