using System;
using System.Collections.Generic;
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
                MessageCacheSize = 100
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

        /* Overload for logging method */
        public static Task Log(string msg, LogSeverity s = LogSeverity.Info, string src = "Unkown"){
            Log(new LogMessage(s,src,msg));
            return Task.CompletedTask;
        }
        /* main program method */
        private async Task MainAsync(){
            
            /* define token var */
            String token = Environment.GetEnvironmentVariable("TOKEN");
            /* subscribe client to logging service */
            _client.Log += Log;
            /* set rich presence properties */
            await Task.Run<Task>(() => _update_game());
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
            _client.MessageDeleted += _client_MessageDeleted;
            _client.MessageUpdated += _client_MessageUpdated;
            /* execute method after new user joins guild */
            _client.UserJoined += _client_user_joined;
            /* execute method after user leaves */
            _client.UserLeft += _client_user_left;
        }

        /* set rich presence properties */
        private async Task _update_game(){
            /* update status each minute */
            while(true){
                var cet = new THONK.Resources.External.PlainsTime_obj();
                await _client.SetGameAsync($"{cet.GetMinLeft()}m to {(!cet.GetIsDay()?"day":"night")}");
                await Task.Delay(60000);
            }
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
            var Result = await _commands.ExecuteAsync(Context, ArgPos, _services);
            /* log to console if command failed */
            if(!Result.IsSuccess){
                await Log(new LogMessage(LogSeverity.Error, "Command Handler", Result.ErrorReason));
            }
        }

        /* method executed after deleting message */
        private async Task _client_MessageDeleted(Cacheable<IMessage, ulong> msg, ISocketMessageChannel channel){
            if(msg.Value.Author.IsBot || msg.Value.Author.IsWebhook)return;
            /////////////////////////////////////////
            // TO-DO return if msg is cmd deleted by bot
            /* position of command prefix */
            int ArgPos = 0;
            string Prefix = THONK.Core.Data.GuildValues.Get.Pefix((channel as SocketGuildChannel).Guild.Id);
            Prefix = Prefix.ToLower();
            if((msg.Value as IUserMessage).HasStringPrefix(Prefix, ref ArgPos))return;
            ////////////////////////////////////////
            var logChannelId = THONK.Core.Data.GuildValues.Get.Channel.Log((channel as SocketGuildChannel).Guild.Id);
            SocketTextChannel log = (channel as SocketTextChannel).Guild.GetTextChannel(logChannelId);
            if(logChannelId!=0){
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(Color.Red);
                if(!msg.HasValue){
                    embed.Description = $"Message was deleted in <#{channel.Id}> but i was unable to retrieve it";
                }else{
                    embed.Description = msg.Value.ToString();
                    embed.WithAuthor(msg.Value.Author);
                    embed.WithTimestamp(DateTimeOffset.UtcNow);
                }
                await log.SendMessageAsync($"Message deleted in <#{channel.Id}>", false,embed);
            }
        }

        /* method executed after updating message */
        private async Task _client_MessageUpdated(Cacheable<IMessage, ulong> msg, SocketMessage msgAfter, ISocketMessageChannel channel){
            if(msg.Value.Author.IsBot || msg.Value.Author.IsWebhook)return;
            // TO-DO return if automatically edited beause of link
            var logChannelId = THONK.Core.Data.GuildValues.Get.Channel.Log((channel as SocketGuildChannel).Guild.Id);
            SocketTextChannel log = (channel as SocketTextChannel).Guild.GetTextChannel(logChannelId);
            if(logChannelId!=0){
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(Color.LightOrange);
                if(!msg.HasValue){
                    embed.Description = $"Message was edited in <#{channel.Id}> but i was unable to retrieve it";
                }else{
                    embed.AddField("Before", msg.Value.ToString());
                    embed.AddField("After", msgAfter.ToString());
                    embed.WithAuthor(msg.Value.Author);
                    embed.WithTimestamp(DateTimeOffset.UtcNow);
                }
                await log.SendMessageAsync($"Message edited in <#{channel.Id}>", false,embed);
            }
        }

        /* method executed after new user joins */
        private async Task _client_user_joined(SocketGuildUser User){
            var Channel = _client.GetChannel(THONK.Core.Data.GuildValues.Get.Channel.General(User.Guild.Id)) as SocketTextChannel;
            await Channel.SendMessageAsync($"Hello <@{User.Id}>! Welcome on **{User.Guild.Name}** Please remember to read the rules and set your nickname here to the same as your warframe name (type '/nick \"your warframe name\")");
            ulong BotLog = THONK.Core.Data.GuildValues.Get.Channel.BotLog(User.Guild.Id);
            await (User as IGuildUser).AddRoleAsync((User as IGuildUser).Guild.Roles.Where(x => x.Name == "Visitor").FirstOrDefault());
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
                await User.Guild.GetTextChannel(BotLog).SendMessageAsync($"{User.Mention} **{User.Username}** ({User.Id}) left");
            }
        }
    }
}