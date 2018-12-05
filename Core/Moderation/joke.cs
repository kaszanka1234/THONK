using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace THONK.Core.Moderation{
    public class Joke : ModuleBase<SocketCommandContext>{
        [Command("unban")]
        public async Task unban(IGuildUser user){
            if(Context.User.Id != 245088596451786754){
                await Context.Channel.SendMessageAsync(":x: insufficient permission");
                return;
            }
            await Context.Channel.SendMessageAsync($"{user.Username} has been unbanned by {Context.User.Mention}");
            await user.AddRoleAsync(Context.Guild.Roles.Where(x => x.Name == "Warlord").FirstOrDefault());
        }
    }
}