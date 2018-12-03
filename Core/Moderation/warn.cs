using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using THONK.Core.Data.GuildValues;

namespace THONK.Core.Moderation{
    [Group("warn"),Summary("Warn user with a gicen reason")]
    public class Warn : ModuleBase<SocketCommandContext> {
        [Group("offline"), Alias("inactive"), Summary("Warn or discharge user for inactivity")]
        public class Offline : ModuleBase<SocketCommandContext>{
            [Command("initiate"), Alias("i"),Summary("Warn an Initiate for idling for too long")]
            public async Task Inititate(SocketGuildUser user, int days){
                if(!User.HasHigherRole(Context.User as IGuildUser, "Lieutenant")){
                    await Context.Channel.SendMessageAsync(":x: Insufficient permissions");
                    return;
                }
                await Context.Message.DeleteAsync();
                var channel = Context.Guild.GetTextChannel(Get.Channel.Announcements(Context.Guild.Id));
                await channel.SendMessageAsync($"{user.Mention} has been offline for {days} days as initiate, if this continues you will get kicked out of the clan");
            }
            [Command("soldier"), Alias("s"),Summary("Warn a Soldier for idling for too long")]
            public async Task Soldier(SocketGuildUser user, int days){
                if(!User.HasHigherRole(Context.User as IGuildUser, "Lieutenant")){
                    await Context.Channel.SendMessageAsync(":x: Insufficient permissions");
                    return;
                }
                await Context.Message.DeleteAsync();
                var channel = Context.Guild.GetTextChannel(Get.Channel.Announcements(Context.Guild.Id));
                await channel.SendMessageAsync($"{user.Mention} has been offline for {days} days, if this continues you will get kicked out of the clan at day 30");
            }
            [Command("kick"), Summary("Kick user for idlong for too long")]
            public async Task Kick(SocketGuildUser user){
                if(!User.HasHigherRole(Context.User as IGuildUser, "General")){
                    await Context.Channel.SendMessageAsync(":x: Insufficient permissions");
                    return;
                }
                await Context.Message.DeleteAsync();
                var channel = Context.Guild.GetTextChannel(Get.Channel.Announcements(Context.Guild.Id));
                /* // broken
                var roles = (user as IGuildUser).RoleIds;
                foreach (var role in roles){
                    await user.RemoveRoleAsync(Context.Guild.GetRole(role));
                }*/
                await channel.SendMessageAsync($"{user.Mention} has been kicked for being offline for too long");
            }
            
        }
    }
}