using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace THONK.Moderation{
    [Group("guild"), Alias("server"), Summary("Manage guild configurations")]
    public class GuildConfiguration : ModuleBase<SocketCommandContext>{
        [Command("register"), Alias("add"), Summary("Manually registers the guild in database")]
        public async Task register(){
            if(THONK.Core.Data.IsGuildInDb.check(Context.Guild.Id)){
                await Context.Channel.SendMessageAsync(":x: This guild is already registered in database, use guild configure to change your options");
            }else{
                await Context.Channel.SendMessageAsync("WIP");
            }
        }
    }
}