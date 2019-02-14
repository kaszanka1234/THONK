using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace THONK.Core.Commands.Moderation{
    public class Order66 : ModuleBase<SocketCommandContext>{
        [Group("execute")]
        public class Execute : ModuleBase<SocketCommandContext>{
            [Group("order")]
            public class Order : ModuleBase<SocketCommandContext>{
                [Command("65")]
                public async Task SixFive(){
                    if(Context.User.Id!=333769079569776642) return;
                    List<SocketGuildUser> users = GetUsers(Context);
                    List<string> usersList = new List<string>();
                    usersList.Add("");
                    int i = 0;
                    users.ForEach(x=>{
                        if(usersList[i].Length<3500){
                            usersList[i] += $"{x.Mention}\n";
                        }else{
                            usersList.Add("");
                            i++;
                        }
                    });
                    usersList.ForEach(async x=>{
                        await Context.Channel.SendMessageAsync(x);
                    });
                }
                [Command("66")]
                public async Task SixSix(){
                    if(Context.User.Id!=333769079569776642) return;
                    var users = GetUsers(Context);
                    Task task = Task.Run(() => kick(users));
                }
                private async Task kick(List<SocketGuildUser> users){
                    users.ForEach(async x =>{
                        string msg = $"{x.Mention} has been executed my lord";
                        await Context.Channel.SendMessageAsync(msg);
                        await x.KickAsync();
                        await Task.Delay(5000);
                    });
                }
                List<SocketGuildUser> GetUsers(SocketCommandContext context){
                    SocketRole unpurgeable = context.Guild.Roles.Where(x => x.Name=="unpurgeable").First();
                    SocketRole inactive = context.Guild.Roles.Where(x => x.Name=="Inactive").First();
                    SocketRole guest = context.Guild.Roles.Where(x => x.Name=="Guest").First();
                    
                    List<SocketGuildUser> users = context.Guild.Users.Where(x => !(x.Roles.Contains(unpurgeable)||x.Roles.Contains(inactive)||x.Roles.Contains(guest))).ToList();
                    return users;
                }
            }
        }
    }
}