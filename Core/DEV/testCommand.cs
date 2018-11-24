using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using THONK.Resources.Database;

namespace THONK.DEV.testCommand{
    /* test and developments commands won't be commented */
    public class testCommand : ModuleBase<SocketCommandContext>{
        [Command("testCommand"), Summary("Development command")]
        public async Task module(){
            using (var db = new SqliteDbContext()){
                
                foreach (var guild in db.Guilds){
                    Console.WriteLine(" - {0} {1} {2}", guild.GuildID, guild.Prefix, guild.ChannelGeneral);
                }
            }
        }
    }
}