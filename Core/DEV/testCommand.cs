using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using THONK.Resources.Database;

namespace THONK.DEV{
    /* test and developments commands won't be commented */
    public class testCommand : ModuleBase<SocketCommandContext>{
        [Command("testCommand"), Alias("test"), Summary("Development command")]
        public async Task module(){
            await Context.Channel.SendMessageAsync("This is a test command");
        }
        [Command("dump")]
        public async Task dump(int count = 50){
            if(!(Context.User as SocketGuildUser).GuildPermissions.ManageMessages){
                await Context.Channel.SendMessageAsync(":x: Insufficent permissions");
                return;
            }
            List<string> users = new List<string>();
            var msgs = await Context.Channel.GetMessagesAsync(count).OfType<IReadOnlyCollection<IMessage>>().ToList();
            foreach(var collection in msgs){
                foreach(var msg in collection){
                    string username = msg.Author.Username.ToString();
                    if(!users.Contains(username)){
                        users.Add(username);
                    }
                }
            }
            string userList = "";
            users.ForEach(x => userList += $"{x}\n");
            await Context.Channel.SendMessageAsync("Users dumped");
            await Context.User.SendMessageAsync(userList);
        }
    }
}