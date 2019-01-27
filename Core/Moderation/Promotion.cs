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
        
        [Command("approve"), Alias("accept"), Summary("Changes user's role from Visitor to initiate (Sergeant)")]
        public async Task approve(SocketGuildUser UserName){
            var User = Context.User as SocketGuildUser;
            if(!THONK.Core.Data.GuildValues.User.HasHigherRole((User as SocketGuildUser), "Sergeant")){
                await Context.Channel.SendMessageAsync(":x: Insufficiet permissions");
                return;
            }else if(!UserName.Roles.Where(x => x.Name=="Visitor").Any() && UserName.Roles.Where(x => x.Name!="@everyone").Any()){
                await Context.Channel.SendMessageAsync(":x: Cannot perform operation on given user");
                return;
            }
            await (UserName as IGuildUser).AddRoleAsync(Context.Guild.Roles.Where(x => x.Name == "Initiate").FirstOrDefault());
            await (UserName as IGuildUser).RemoveRoleAsync(Context.Guild.Roles.Where(x => x.Name == "Visitor").FirstOrDefault());
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
            [Command("soldier"), Summary("Promote member to soldier (Lieutenant)")]
            public async Task soldier(SocketGuildUser UserName){
                var User = Context.User as SocketGuildUser;
                if(!THONK.Core.Data.GuildValues.User.HasHigherRole((User as SocketGuildUser), "Lieutenant")){
                    await Context.Channel.SendMessageAsync(":x: Insufficiet permissions");
                    return;
                }else if(!THONK.Core.Data.GuildValues.User.HasHigherRole((UserName as SocketGuildUser), "Initiate")){
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
            }else if(user != Context.User && !User.HasHigherRole(Context.User as SocketGuildUser, "Lieutenant")){
                await Context.Channel.SendMessageAsync(":x: Insufficient permissions");
                return;
            }
            await user.RemoveRolesAsync((user as SocketGuildUser).Roles.Where(x => x.Name.Contains("MR")));
            await user.AddRoleAsync(Context.Guild.Roles.Where(x => x.Name == $"MR{rank}").FirstOrDefault());
            await Context.Channel.SendMessageAsync($"{(user.Nickname==null?user.Username:user.Nickname)} has been assigned MR{rank}");
        }
        [Command("mastery rank"), Alias("mr"), Summary("Set user mr")]
        public async Task mr([Remainder]string str=""){
            await Context.Channel.SendMessageAsync("You have to specify rank eg. /user mr 15");
        }
        [Command(""), Summary("")]
        public async Task mr_alias([Remainder]string str=""){
            switch (str){
                case "mr1":
                    await mr(1);
                    break;
                case "mr2":
                    await mr(2);
                    break;
                case "mr3":
                    await mr(3);
                    break;
                case "mr4":
                    await mr(4);
                    break;
                case "mr5":
                    await mr(5);
                    break;
                case "mr6":
                    await mr(6);
                    break;
                case "mr7":
                    await mr(7);
                    break;
                case "mr8":
                    await mr(8);
                    break;
                case "mr9":
                    await mr(9);
                    break;
                case "mr10":
                    await mr(10);
                    break;
                case "mr11":
                    await mr(11);
                    break;
                case "mr12":
                    await mr(12);
                    break;
                case "mr13":
                    await mr(13);
                    break;
                case "mr14":
                    await mr(14);
                    break;
                case "mr15":
                    await mr(15);
                    break;
                case "mr16":
                    await mr(16);
                    break;
                case "mr17":
                    await mr(17);
                    break;
                case "mr18":
                    await mr(18);
                    break;
                case "mr19":
                    await mr(19);
                    break;
                case "mr20":
                    await mr(20);
                    break;
                case "mr21":
                    await mr(21);
                    break;
                case "mr22":
                    await mr(22);
                    break;
                case "mr23":
                    await mr(23);
                    break;
                case "mr24":
                    await mr(24);
                    break;
                case "mr25":
                    await mr(25);
                    break;
                case "mr26":
                    await mr(26);
                    break;
                case "mr27":
                    await mr(27);
                    break;
                case "mr28":
                    await mr(28);
                    break;
                case "mr29":
                    await mr(29);
                    break;
                case "mr30":
                    await mr(30);
                    break;
                default:
                    break;
            }
        }

        [Command("kick"), Summary("Kick")]
            public async Task Kick(SocketGuildUser user,params string[] args){
                bool force = false;
                string reason="";
                foreach(var arg in args){
                    if(arg=="-f"){
                        force=true;
                    }else{
                        reason += " "+arg;
                    }
                }
                if(!User.HasHigherRole(Context.User as SocketGuildUser, "General")){
                    await Context.Channel.SendMessageAsync(":x: Insufficient permissions");
                    return;
                }
                if(!User.HasHigherRole(Context.User as SocketGuildUser, "Warlord") && force){
                    await Context.Channel.SendMessageAsync(":x: Insufficient permissions, only Warlord can do that");
                    return;
                }
                if(User.HasHigherRole(user, "Sergeant") && !force){
                    await Context.Channel.SendMessageAsync("User is member of staff, if you still want to kick them use /user kick @user reason -f");
                    return;
                }
                if(user.Roles.Where(x => x.Name=="Inactive").Any() && !force){
                    await Context.Channel.SendMessageAsync("User is marked as inactive, if you still want to kick them use /user kick @user reason -f");
                    return;
                }
                //await Context.Message.DeleteAsync();
                var channel = Context.Guild.GetTextChannel(Get.Channel.Announcements(Context.Guild.Id));
                var roles = user.Roles;
                foreach (SocketRole role in roles){
                    if(role.Name!="@everyone"){
                        await user.RemoveRoleAsync(role as IRole);
                    }
                }
                await channel.SendMessageAsync($"{user.Mention} has been kicked from clan{(reason==""?"":$" for{reason}")}, if you want to rejoin later contact any sergeant or higher");
                ulong BotLog = Get.Channel.BotLog(Context.Guild.Id);
                if(BotLog!=0){
                    await Context.Guild.GetTextChannel(BotLog).SendMessageAsync($"{user.Mention} was kicked by {Context.User.Mention} for{reason}");
                }
            }
    }
}
