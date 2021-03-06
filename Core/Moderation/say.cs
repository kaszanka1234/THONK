using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace THONK.Core.Moderation{
    public class Say : ModuleBase<SocketCommandContext>{
        [Command("say"),Alias("tell"),Summary("Say something as bot")]
        public async Task say([Remainder]string message){
            if(Context.User.Id==419880181101232129){
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} is a looser");
            }
            if(!THONK.Core.Data.GuildValues.User.HasHigherRole((Context.User as SocketGuildUser), "Lieutenant")){
                await Context.Channel.SendMessageAsync(":x: Insufficient permissions");
                return;
            }
            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync(message);
        }
        [Command("announce"), Summary("Send a message to announcements channel (Lieutenant)")]
        public async Task announce([Remainder]string message){
            if(!THONK.Core.Data.GuildValues.User.HasHigherRole((Context.User as SocketGuildUser), "Lieutenant")){
                await Context.Channel.SendMessageAsync(":x: Insufficient permissions");
                return;
            }
            await Context.Message.DeleteAsync();
            var channel = Context.Guild.GetTextChannel(THONK.Core.Data.GuildValues.Get.Channel.Announcements(Context.Guild.Id));
            await channel.SendMessageAsync(message);
        }
        [Command("hidden say"), Alias("hsay"),Summary("Say something as bot from a hidden channel")]
        public async Task hsay([Remainder]string message){
            if(!THONK.Core.Data.GuildValues.User.HasHigherRole((Context.User as SocketGuildUser), "Warlord"))return;
            await Context.Message.DeleteAsync();
            var channel = Context.Guild.GetTextChannel(THONK.Core.Data.GuildValues.Get.Channel.General(Context.Guild.Id));
            await channel.SendMessageAsync(message);
        }
        [Command("w"), Alias("dm", "priv"), Summary("send a DM to user")]
        public async Task dm(SocketUser user, [Remainder]string message){
            if(!THONK.Core.Data.GuildValues.User.HasHigherRole((Context.User as SocketGuildUser), "Warlord"))return;
            await user.SendMessageAsync(message);
        }
    }
}
