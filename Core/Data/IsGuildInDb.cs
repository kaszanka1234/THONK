using System;
using System.Linq;
using System.Threading.Tasks;
using THONK.Resources.Database;

namespace THONK.Core.Data{
    public class IsGuildInDb{
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