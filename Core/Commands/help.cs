using System;
using System.IO;
using System.Threading.Tasks;
using Discord.Commands;

namespace THONK.Core.Commands{
    public class Help : ModuleBase<SocketCommandContext>{
        [Command("help"), Alias("hlep")]
        public async Task help(){
            //
        }
    }
}