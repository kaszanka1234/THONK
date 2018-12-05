using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using THONK.Core.Data.GuildValues;

namespace THONK.Core.Moderation{
    [Group("user"), Summary("Changes user's role")]
    public class Promotion : ModuleBase<SocketCommandContext>{
        
        [Command("approve"), Alias("accept"), Summary("Changes user's role from Guest to initiate")]
        public async Task approve(SocketGuildUser UserName){
            var User = Context.User as SocketGuildUser;
            if(!THONK.Core.Data.GuildValues.User.HasHigherRole((User as IGuildUser), "Sergeant")){
                await Context.Channel.SendMessageAsync(":x: Insufficiet permissions");
                return;
            }else if(!THONK.Core.Data.GuildValues.User.HasHigherRole((UserName as IGuildUser), "Guest")){
                await Context.Channel.SendMessageAsync(":x: Cannot perform operation on given user");
                return;
            }
            await (UserName as IGuildUser).AddRoleAsync(Context.Guild.Roles.Where(x => x.Name == "Initiate").FirstOrDefault());
            await (UserName as IGuildUser).RemoveRoleAsync(Context.Guild.Roles.Where(x => x.Name == "Guest").FirstOrDefault());
            string UserNickname = (UserName as IGuildUser).Nickname==null?UserName.Username:(UserName as IGuildUser).Nickname;
            string StaffNickname = (Context.User as IGuildUser).Nickname==null?Context.User.Username:(Context.User as IGuildUser).Nickname;
            string message = $"**{UserNickname}** is now a member of the clan!\nWelcome and have fun!";
            await Context.Guild.TextChannels.Where(x => x.Id == Get.Channel.General(Context.Guild.Id)).FirstOrDefault().SendMessageAsync(message);
            ulong BotLog = THONK.Core.Data.GuildValues.Get.Channel.BotLog(User.Guild.Id);
            if(!(BotLog==0)){
                await User.Guild.GetTextChannel(BotLog).SendMessageAsync($"<@{UserName.Id}> (*{UserNickname}*, {UserName.Id} was approved by **{StaffNickname}**");
            }
        }
        [Group("promote"), Summary("Promotes user to higher rank")]
        public class Promote : ModuleBase<SocketCommandContext>{
            [Command("soldier"), Summary("Promote to soldier")]
            public async Task soldier(SocketGuildUser UserName){
                var User = Context.User as SocketGuildUser;
                if(!THONK.Core.Data.GuildValues.User.HasHigherRole((User as IGuildUser), "Lieutenant")){
                    await Context.Channel.SendMessageAsync(":x: Insufficiet permissions");
                    return;
                }else if(!THONK.Core.Data.GuildValues.User.HasHigherRole((UserName as IGuildUser), "Initiate")){
                    await Context.Channel.SendMessageAsync(":x: Cannot perform operation on given user");
                    return;
                }
                await (UserName as IGuildUser).AddRoleAsync(Context.Guild.Roles.Where(x => x.Name == "Soldier").FirstOrDefault());
                await (UserName as IGuildUser).RemoveRoleAsync(Context.Guild.Roles.Where(x => x.Name == "Initiate").FirstOrDefault());
                string UserNickname = (UserName as IGuildUser).Nickname==null?UserName.Username:(UserName as IGuildUser).Nickname;
                string message = $"**{UserNickname}** has been promoted to a rank of soldier";
                await Context.Guild.TextChannels.Where(x => x.Id == Get.Channel.General(Context.Guild.Id)).FirstOrDefault().SendMessageAsync(message);
            }
        }
        [Group("demote"), Summary("Demotes user to lower rank")]
        public class demote : ModuleBase<SocketCommandContext>{
            // TODO
        }
        [Command("mastery rank"), Alias("mr"), Summary("Set user mr")]
        public async Task mr(int rank, IGuildUser user = null){
            if(rank>30 || rank<1){
                await Context.Channel.SendMessageAsync(":x: Rank must be from 1 to 30");
                return;
            }
            if(user==null){
                user = Context.User as IGuildUser;
            }else if(user != Context.User && !User.HasHigherRole(Context.User as IGuildUser, "Lieutenant")){
                await Context.Channel.SendMessageAsync(":x: Insufficient permissions");
                return;
            }
            await user.RemoveRoleAsync(Context.Guild.Roles.Where(x => x.Name.Contains("MR")).FirstOrDefault());
            await user.AddRoleAsync(Context.Guild.Roles.Where(x => x.Name == $"MR{rank}").FirstOrDefault());
            await Context.Channel.SendMessageAsync($"{(user.Nickname==null?user.Username:user.Nickname)} has been assigned MR{rank}");
        }
    }
}
