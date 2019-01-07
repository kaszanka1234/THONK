using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace THONK.Core.Commands{
    public class PlainsTime : ModuleBase<SocketCommandContext>{
        [Command("plainstime"), Alias("plains time", "time plains", "cetus time"), Summary("Shows current time on Cetus")]
        public async Task time(){
            var cetus = new THONK.Resources.External.PlainsTime_obj();
            await Context.Channel.SendMessageAsync($"**{(cetus.GetIsDay()?"DAY":"NIGHT")}**\nTime left: {cetus.GetTimeLeft()}");
        }
    }
}