using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace THONK.Core.Commands{
    public class Vote : ModuleBase<SocketCommandContext> {
        [Command("vote")]
        public async Task vote(){
            try{
            IEmote emote1 = new Emoji("\u2705");
            IEmote emote2 = new Emoji("\u274C");
            await Context.Message.AddReactionAsync(emote1);
            await Task.Delay(2500);
            await Context.Message.AddReactionAsync(emote2);
            }catch(Exception e){
                Console.WriteLine(e.ToString());
            }
        }
    }
}