using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace THONK.Core.Commands{
    /* ping class extending module of type socket command */
    public class ping : ModuleBase<SocketCommandContext>{
        /* set command's properties
         * [command's name, aliases of command, description of command] */
        [Command("ping"), /*Alias("ping", "pong", "piong"), */Summary("Ping command")]
        public async Task module(){
            await Context.Channel.SendMessageAsync("pong!");
        }
    }
}