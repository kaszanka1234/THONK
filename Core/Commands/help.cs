using System;
using System.IO;
using System.Threading.Tasks;
using Discord.Commands;

namespace THONK.Core.Commands{
    public class Help : ModuleBase<SocketCommandContext>{
        [Command("help"), Alias("hlep")]
        public async Task help(){
            string msg = "";
            using (var fileStream = File.OpenRead("help.txt"))
  using (var streamReader = new StreamReader(fileStream)) {
    String line;
    while ((line = streamReader.ReadLine()) != null){
        msg += line+"\n";
    }
  }
            await Context.Channel.SendMessageAsync(msg);
        }
    }
}