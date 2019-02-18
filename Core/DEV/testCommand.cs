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
    }
}