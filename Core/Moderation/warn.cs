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
        [Group("offline"), Alias("inactive"), Summary("Warn or discharge user for inactivity")]
        public class Offline : ModuleBase<SocketCommandContext>{
            [Command("initiate"), Alias("i"),Summary("Warn an Initiate for idling for too long (lieutenant)")]
            public async Task Inititate(SocketGuildUser user, int days){
                if(!User.HasHigherRole(Context.User as SocketGuildUser, "Lieutenant")){
                    await Context.Channel.SendMessageAsync(":x: Insufficient permissions");
                    return;
                }
                await Context.Message.DeleteAsync();
                var channel = Context.Guild.GetTextChannel(Get.Channel.Announcements(Context.Guild.Id));
                await channel.SendMessageAsync($"{user.Mention} has been offline for {days} days as initiate, if this continues you will get kicked out of the clan");
            }
            [Command("initiate"), Alias("i")]
            public async Task Inititate([Remainder]string str=""){
                await Context.Channel.SendMessageAsync("Correct syntax is /warn inactive initiate @user [days]");
            }
            [Command("soldier"), Alias("s"),Summary("Warn a Soldier for idling for too long (lieutenant)")]
            public async Task Soldier(SocketGuildUser user, int days){
                if(!User.HasHigherRole(Context.User as SocketGuildUser, "Lieutenant")){
                    await Context.Channel.SendMessageAsync(":x: Insufficient permissions");
                    return;
                }
                await Context.Message.DeleteAsync();
                var channel = Context.Guild.GetTextChannel(Get.Channel.Announcements(Context.Guild.Id));
                await channel.SendMessageAsync($"{user.Mention} has been offline for {days} days, if this continues you will get kicked out of the clan at day 30");
            }
            [Command("soldier"), Alias("s")]
            public async Task Soldier([Remainder]string str=""){
                await Context.Channel.SendMessageAsync("Correct syntax is /warn inactive soldier @user [days]");
            }
        }
    }
}