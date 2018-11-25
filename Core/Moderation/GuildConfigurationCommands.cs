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
                await SetGuildValues.Register(Context.Guild.Id, Context.Channel.Id);
                await Context.Channel.SendMessageAsync("Server successfully registered!");
            }
        }

        /* Command group for changing settings */
        [Group("reconfigure"), Alias("change", "set", "configure", "config"), Summary("Changes guild settings")]
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
                await SetGuildValues.Prefix(Context.Guild.Id, Prefix);
                await Context.Channel.SendMessageAsync($"New command prefix is {Prefix} eg. {Prefix}ping");
            }
            [Group("channel"), Summary("Update channels in DB")]
            public class Channels : ModuleBase<SocketCommandContext>{
                [Command("general"), Alias("main"), Summary("Set general channel")]
                public async Task General(){
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
                    await SetGuildValues.Channel.General(Context.Guild.Id, Context.Channel.Id);
                    await Context.Channel.SendMessageAsync($"<#{Context.Channel.Id}> sucessfuly set as General channel");
                }
                [Command("announcements"), Summary("Set general channel")]
                public async Task Announcements(){
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
                    await SetGuildValues.Channel.Announcements(Context.Guild.Id, Context.Channel.Id);
                    await Context.Channel.SendMessageAsync($"<#{Context.Channel.Id}> sucessfuly set as Announcements channel");
                }
                [Command("botlog"), Alias("bot", "bot log"), Summary("Set general channel")]
                public async Task BotLog(){
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
                    await SetGuildValues.Channel.BotLog(Context.Guild.Id, Context.Channel.Id);
                    await Context.Channel.SendMessageAsync($"<#{Context.Channel.Id}> sucessfuly set as Bot Log channel");
                }
                [Command("log"), Summary("Set general channel")]
                public async Task Log(){
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
                    await SetGuildValues.Channel.Log(Context.Guild.Id, Context.Channel.Id);
                    await Context.Channel.SendMessageAsync($"<#{Context.Channel.Id}> sucessfuly set as Log channel");
                }
                
            }
        }
        
    }
}