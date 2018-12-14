using System;
using System.IO;
using System.Linq;
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

        /* Default constructor */
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
            string logging = $"{DateTime.Now,-19} [{msg.Severity,8}] {msg.Source}: {msg.Message}";
            Console.WriteLine(logging);
            Console.ForegroundColor = cc;
            using (StreamWriter s = new StreamWriter("latest.log",true)){
                s.WriteLineAsync(logging);
            }
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
            /* execute method after new user joins guild */
            _client.UserJoined += _client_user_joined;
            /* execute method after user lefts */
            _client.UserLeft += _client_user_left;
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
            string Prefix = THONK.Core.Data.GuildValues.Get.Pefix(Context.Guild.Id);
            Prefix = Prefix.ToLower();
            /* return if message doesn't have a prefix */
            if(!Message.HasStringPrefix(Prefix, ref ArgPos))return;
            /* get the result of executed command */
            var Result = await _commands.ExecuteAsync(Context, ArgPos);
            /* log to console if command failed */
            if(!Result.IsSuccess){
                await Log(new LogMessage(LogSeverity.Error, "Command Handler", Result.ErrorReason));
            }
        }
        /* method executed after new user joins */
        private async Task _client_user_joined(SocketGuildUser User){
            var Channel = _client.GetChannel(THONK.Core.Data.GuildValues.Get.Channel.General(User.Guild.Id)) as SocketTextChannel;
            await Channel.SendMessageAsync($"Hello <@{User.Id}>! Welcome on **{User.Guild.Name}** Please remember to read the rules and set your nickname here (right-click your name and 'change nickname') to the same as you use in game");
            ulong BotLog = THONK.Core.Data.GuildValues.Get.Channel.BotLog(User.Guild.Id);
            await (User as IGuildUser).AddRoleAsync((User as IGuildUser).Guild.Roles.Where(x => x.Name == "Guest").FirstOrDefault());
            if(!(BotLog==0)){
                await User.Guild.GetTextChannel(BotLog).SendMessageAsync($"<@{User.Id}> joined");
            }
        }
        /* method executed after user leaves */
        private async Task _client_user_left(SocketGuildUser User){
            var Channel = _client.GetChannel(THONK.Core.Data.GuildValues.Get.Channel.General(User.Guild.Id)) as SocketTextChannel;
            await Channel.SendMessageAsync($"**{User.Username}** has left\nPress F to pay respects");
            ulong BotLog = THONK.Core.Data.GuildValues.Get.Channel.BotLog(User.Guild.Id);
            if(!(BotLog==0)){
                await User.Guild.GetTextChannel(BotLog).SendMessageAsync($"<@{User.Id}> left");
            }
        }
    }
}