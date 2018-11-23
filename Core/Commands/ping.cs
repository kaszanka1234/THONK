using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace THONK.Core.Commands{
    public class ping : ModuleBase<SocketCommandContext>{
        [Command("ping"), Alias("ping"), Summary("Ping command")]
        public async Task module(){
            await Context.Channel.SendMessageAsync("pong!");
        }
    }
}