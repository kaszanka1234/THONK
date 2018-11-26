using System;
using System.Linq;
using System.Threading.Tasks;
using THONK.Resources.Database;

namespace THONK.Core.Data{
    public class Registration{
        /* register guild in DB */
        public static async Task Register(ulong id, ulong channel){
            /* check if guild exists in DB */
            if(check(id)){
                throw new Exception("Guild already registered!");
            }
            /* Add new record for guild */
            using(var db = new SqliteDbContext()){
                Guild guild = new Guild{
                    GuildID = id,
                    ChannelGeneral = channel,
                    Prefix = "/",
                    ChannelAnnouncements = channel,
                    ChannelBotLog = 0,
                    ChannelLog = 0
                };
                await db.Guilds.AddAsync(guild);
                await db.SaveChangesAsync();
            }
        }
        /* Remove guild from DB */
        public static async Task Unregister(ulong id){
            /* Check if guild is in DB */
            if(!check(id)){
                throw new Exception("No Guild with given ID");
            }
            /* Delete guild's record */
            using (var db = new SqliteDbContext()){
                var guild = new Guild{GuildID = id};
                db.Guilds.Attach(guild);
                db.Guilds.Remove(guild);
                await db.SaveChangesAsync();
            }
        }
        /* Look in database for current guild */
        public static bool check(ulong guildId){
            using (var db = new SqliteDbContext()){
                if(db.Guilds.Find(guildId) != null){
                    return true;
                }
                return false;
            }
        }
    }
}