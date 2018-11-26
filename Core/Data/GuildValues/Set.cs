using System;
using System.Threading.Tasks;
using THONK.Resources.Database;

namespace THONK.Core.Data.GuildValues{
    /* Set values of guild in DB */
    public class Set{
        
        /* Set guild's command prefix */
        public static async Task Prefix(ulong GuildId, string Prefix){
            /* if no prefix is passed throw exception */
            if(Prefix == null){
                throw new Exception("Prefix cannot be null");
            }
            /* save new prefix to DB */
            using (var db = new SqliteDbContext()){
                var Guild = await db.Guilds.FindAsync(GuildId);
                Guild.Prefix = Prefix;
                db.Guilds.Update(Guild);
                await db.SaveChangesAsync();
            }
        }
        public class Channel{
            /* set guild general channel */
            public static async Task General(ulong guild, ulong channel){
                using (var db = new SqliteDbContext()){
                    var Guild = await db.Guilds.FindAsync(guild);
                    Guild.ChannelGeneral = channel;
                    db.Guilds.Update(Guild);
                    await db.SaveChangesAsync();
                }
            }
            /* set guild announcements channel */
            public static async Task Announcements(ulong guild, ulong channel){
                using (var db = new SqliteDbContext()){
                    var Guild = await db.Guilds.FindAsync(guild);
                    Guild.ChannelAnnouncements = channel;
                    db.Guilds.Update(Guild);
                    await db.SaveChangesAsync();
                }
            }
            /* set guild bot log channel */
            public static async Task BotLog(ulong guild, ulong channel){
                using (var db = new SqliteDbContext()){
                    var Guild = await db.Guilds.FindAsync(guild);
                    Guild.ChannelBotLog = channel;
                    db.Guilds.Update(Guild);
                    await db.SaveChangesAsync();
                }
            }
            /* set guild log channel */
            public static async Task Log(ulong guild, ulong channel){
                using (var db = new SqliteDbContext()){
                    var Guild = await db.Guilds.FindAsync(guild);
                    Guild.ChannelLog = channel;
                    db.Guilds.Update(Guild);
                    await db.SaveChangesAsync();
                }
            }
        }
        
        
    }
}