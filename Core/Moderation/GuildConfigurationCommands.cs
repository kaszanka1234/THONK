using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using THONK.Resources.Database;
using THONK.Core.Data;

namespace THONK.Moderation{
    /* Command group for managing guild */
    [Group("guild"), Alias("server"), Summary("Manage server")]
    public class GuildConfiguration : ModuleBase<SocketCommandContext>{
        /* Create new record with guild configurations */
        [Command("register"), Alias("add"), Summary("Manually registers the guild")]
        public async Task register(){
            /* Return if guild is already in database */
            if(THONK.Core.Data.IsGuildInDb.check(Context.Guild.Id)){
                await Context.Channel.SendMessageAsync(":x: This guild is already registered, use guild configure to change your options");
                return;
            }else{
                await Context.Channel.SendMessageAsync("WIP");
            }
        }

        /* Command group for changing settings */
        [Group("reconfigure"), Alias("change", "set"), Summary("Changes guild settings")]
        public class Reconfiguration : ModuleBase<SocketCommandContext>{
            [Command("prefix"), Alias("cmd"), Summary("Set new command prefix")]
            public async Task Prefix(string Prefix){
                /* Check if user has manage permission */
                SocketGuildUser GuildUser = Context.User as SocketGuildUser;
                if(!GuildUser.GuildPermissions.ManageGuild){
                    await Context.Channel.SendMessageAsync(":x: Insufficient permissions, you need Manage server permission to do this");
                    return;
                }
                /* Check if guild is registered */
                if(!IsGuildInDb.check(Context.Guild.Id)){
                    await Context.Channel.SendMessageAsync(":x: Your Server is not registered, please register it manually");
                    return;
                }
                /* Check if prefix is specified */
                if(Prefix==null){
                    await Context.Channel.SendMessageAsync(":x: You have to specify a prefix");
                    return;
                }
                /* Change the prefix */
                SetGuildValues.Prefix(Context.Guild.Id, Prefix);
                await Context.Channel.SendMessageAsync($"New command prefix is {Prefix} eg. {Prefix}ping");
            }
        }
        
    }
}