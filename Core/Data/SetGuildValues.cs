using System;
using System.Threading.Tasks;
using THONK.Resources.Database;

namespace THONK.Core.Data{
    /* Set values of guild in DB */
    public class SetGuildValues{
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
    }
}