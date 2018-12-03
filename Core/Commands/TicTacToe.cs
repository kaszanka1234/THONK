using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace THONK.Core.Commands{
    [Group("TicTacToe"),Alias("ttt"),Summary("Play TicTacToe with me")]
    public class Play : ModuleBase<SocketCommandContext>{
        [Command("")]
        public async Task Place(){
            await Context.Channel.SendMessageAsync("Game is still being implemented");
        }
    }
}