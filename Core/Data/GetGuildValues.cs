using System;
using THONK.Resources.Database;

namespace THONK.Core.Data{
    /* Get various values about the guild from database */
    public class GetGuildValues{
        /* get command prefix */
        public static string GetPefix(ulong guildId){
            using (var db = new SqliteDbContext()){
                var guild = db.Guilds.Find(guildId);
                /* if no record is found return default prefix */
                if(guild == null){
                    return "/";
                }else{
                    return guild.Prefix;
                }
            }
        }
        public class Channel{
            /* get general channel */
            public static ulong General(ulong guildId){
                using (var db = new SqliteDbContext()){
                    ulong channel = db.Guilds.Find(guildId).ChannelGeneral;
                    return channel;
                }
            }
            
            /* get announcements channel */
            public static ulong Announcements(ulong guildId){
                using (var db = new SqliteDbContext()){
                    ulong channel = db.Guilds.Find(guildId).ChannelAnnouncements;
                    return channel;
                }
            }
            /* get general channel */
            public static ulong BotLog(ulong guildId){
                using (var db = new SqliteDbContext()){
                    ulong channel = db.Guilds.Find(guildId).ChannelBotLog;
                    return channel;
                }
            }
            /* get general channel */
            public static ulong Log(ulong guildId){
                using (var db = new SqliteDbContext()){
                    ulong channel = db.Guilds.Find(guildId).ChannelAnnouncements;
                    return channel;
                }
            }
        }
    }
}