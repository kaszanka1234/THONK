using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace THONK.Core.Moderation{
    [Group("delete")]
    public class Delete : ModuleBase<SocketCommandContext>{
        [Command("")]
        public async Task Del(int numMessages=1){
            if(!(Context.User as SocketGuildUser).GuildPermissions.ManageMessages){
                await Context.Channel.SendMessageAsync(":x: Insufficient permissions");
                return;
            }
            await Context.Message.DeleteAsync();
            int counter=0;
            bool failed=false;
            IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(numMessages).Flatten();
            foreach (IMessage message in messages){
                Task task = message.DeleteAsync();
                await task;
                if(task.IsCompletedSuccessfully){
                    counter++;
                }else{
                    failed=true;
                }
            }
            await Task.Run(() => Deleted(counter,failed));
        }
        private async Task Deleted(int numMessages,bool failed=false){try{
            string message = $"Deleted {numMessages} messages{(failed?" and i could not delete some messages":"")}";
            IMessage confirmation = await Context.Channel.SendMessageAsync(message);
            await Task.Delay(5000);
            await confirmation.DeleteAsync();}catch(System.Exception e){
            await THONK.Program.Log(e.ToString());
        }
        }
    }
}