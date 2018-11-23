using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using THONK.Resources.Database;

namespace THONK.DEV.testCommand{
    public class testCommand : ModuleBase<SocketCommandContext>{
        [Command("testCommand"), Summary("Development command")]
        public async Task module(){
            using (var db = new SqliteDbContext()){
                /*
                db.Guilds.Add(new Guild {
                    GuildID = 488843604731887641,
                    Prefix = "?dev",
                    ChannelGeneral = 514900273299193868,
                    ChannelAnnouncements = 514900273299193868,
                    ChannelBotLog = 514900273299193868,
                    ChannelLog = 514900273299193868,
                    RoleWarlord = 0,
                    RoleGeneral = 0,
                    RoleLieutenant = 0,
                    RoleSergeant = 0,
                    RoleSoldier = 0,
                    RoleInitiate = 0,
                    RoleGuest = 0
                });
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved: ", count);
                */

                foreach (var guild in db.Guilds){
                    Console.WriteLine(" - {0} {1} {2}", guild.GuildID, guild.Prefix, guild.ChannelGeneral);
                }
            }
        }
    }
}