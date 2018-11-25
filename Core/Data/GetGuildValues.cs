using System;
using THONK.Resources.Database;

namespace THONK.Core.Data{
    /* Get various values about the guild from database */
    public class GetGuildValues{
        /* get command prefix */
        public static string GetPefix(ulong guildId){
            using (var db = new SqliteDbContext()){
                String Prefix = db.Guilds.Find(guildId).Prefix;
                /* if no record is found return default prefix */
                if(Prefix == null){
                    return "/";
                }else{
                    return Prefix;
                }
            }
        }
    }
}