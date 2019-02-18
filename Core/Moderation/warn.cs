using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using THONK.Core.Data.GuildValues;

namespace THONK.Core.Moderation{
    [Group("warn"),Summary("Warn user with a given reason")]
    public class Warn : ModuleBase<SocketCommandContext> {
        [Command("offline"), Alias("inactive"), Summary("Warn for inactivity")]
        public async Task Offline(SocketGuildUser user, int days=0){
            if(!User.HasHigherRole(Context.User as SocketGuildUser, "Lieutenant")){
                await Context.Channel.SendMessageAsync(":x: Insufficient permissions");
                return;
            }
            await Context.Message.DeleteAsync();
            var channel = await user.GetOrCreateDMChannelAsync();
            await channel.SendMessageAsync($"You has been offline for {(days==0?"too long":$"{days} days")}, if this continues you will get kicked out of the clan");
        }
        [Command("offline"), Alias("inactive")]
        public async Task Offline([Remainder]string str=""){
            await Context.Channel.SendMessageAsync("Correct syntax is /warn offline @user [days]");
        }
        [Command("")]
        public async Task Cmds(){
            await Context.Channel.SendMessageAsync("correct comands are:\n/warn offline @user [days]");
        }
    }
}