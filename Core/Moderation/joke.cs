using System;
using System.Collections;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace THONK.Core.Moderation{
    public class Joke : ModuleBase<SocketCommandContext>{
        [Command("unban")]
        public async Task unban([Remainder]string asd){
            await Context.Channel.SendMessageAsync(":x: insufficient permission");
        }
    }
}