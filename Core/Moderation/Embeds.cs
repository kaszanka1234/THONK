using System;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using THONK.Core.Data.GuildValues;

namespace THONK.Core.Moderation{
    public class Embed : ModuleBase<SocketCommandContext>{
        [Command("rules")]
        public async Task Rules(){
            //EmbedBuilder[] builders = {new EmbedBuilder(),new EmbedBuilder(),new EmbedBuilder()};
            EmbedBuilder[] builders = new EmbedBuilder[4];
            for(int i=0;i<builders.Length;i++){
                builders[i] = new EmbedBuilder();
            }
            string[] dRules = {
                "Be respectful.",
                "Sending/Linking any harmful material such as viruses, IP grabbers or harmware results in an immediate and permanent ban.",
                "Use proper grammar and spelling and don't spam.",
                "Usage of excessive extreme innapropriate langauge is prohibited.",
                "Usage of excessive caps is prohibited",
                "Mentioning @everyone, the Moderators or a specific person without proper reason is prohibited.",
                "Don't post someone's personal information without permission.",
                "If you are offline on discord for more than 7 days you can be kicked out of clan. If you can't login for an extended period of time contact any officer or higher.",
                "Please use the same name that you use in-game. Do not use fake nickname.",
                "NO advertising of any kind on our Discord server, This includes links to sites that earn you money. Admins decide what is allowed and what is not. DONT asume anything check with an admin first.",
                "No rules apply to #memes"
            };
            string[] gRules = {
                "All rulles that apply to discord also apply to in game chat",
                "If you are inactive for more than 30 days you can be kicked out of clan, if you can't play for extended period of time contact any officer or higher",
                "To reach the rank of soldier in clan you have to be at least MR3 and be a member of the clan for at least 2 days, if you are inactive in these 2 days you can be kicked out",
                "Higher ranks are given by the highest ranked members of the clan as they are needed, don't ask for a higher rank unless you have a good reason for it"
            };
            foreach (EmbedBuilder builder in builders){
                builder.WithAuthor(Context.Guild.GetUser(333769079569776642));
                builder.WithFooter("Last updated");
                builder.WithCurrentTimestamp();
            }
            builders[0].WithTitle("First and foremost");
            builders[0].WithDescription("You need the discord app and be on this server");
            builders[0].WithColor(0x0000ff);
            builders[1].WithTitle("Discord rules");
            for(int i=0;i<dRules.Length;i++){
                builders[1].Description += $"**{i+1}.** " + dRules[i] + "\n";
            }
            builders[1].WithColor(0xff0000);
            builders[2].WithTitle("In game rules");
            for(int i=0;i<gRules.Length;i++){
                builders[2].Description += $"**{i+1}.** " + gRules[i] + "\n";
            }
            builders[2].WithColor(0xff00ff);
            builders[3].WithColor(0xffff00);
            builders[3].WithTitle("Got any suggestions?");
            builders[3].WithDescription($"Message {Context.Client.GetUser(333769079569776642).Mention} or post them in suggestions channel");
            //await Context.Channel.SendMessageAsync("As a fallback you can message me with \"rules\" to get rules in text-only format");
            for(int i=0;i<builders.Length;i++){
                await Context.Channel.SendMessageAsync("",false, builders[i]);
                await Task.Delay(1000);
            }
        }
        [Command("ranks"), Alias("ranks list", "rank list")]
        public async Task ranks(){
            if(!User.HasHigherRole(Context.User as IGuildUser, "Warlord")){
                await Context.Channel.SendMessageAsync(":x: Insufficient permissions");
                return;
            }
            EmbedBuilder[] builders = new EmbedBuilder[8];
            for(int i=0; i<builders.Length;i++){
                builders[i] = new EmbedBuilder();
            }
            SocketRole[] roles = new SocketRole[8];
            roles[0] = Context.Guild.Roles.Where(x => x.Name == "Warlord").FirstOrDefault();
            roles[1] = Context.Guild.Roles.Where(x => x.Name == "General").FirstOrDefault();
            roles[2] = Context.Guild.Roles.Where(x => x.Name == "Lieutenant").FirstOrDefault();
            roles[3] = Context.Guild.Roles.Where(x => x.Name == "Sergeant").FirstOrDefault();
            roles[4] = Context.Guild.Roles.Where(x => x.Name == "Soldier").FirstOrDefault();
            roles[5] = Context.Guild.Roles.Where(x => x.Name == "Guest").FirstOrDefault();
            roles[6] = Context.Guild.Roles.Where(x => x.Name == "Initiate").FirstOrDefault();
            roles[7] = Context.Guild.Roles.Where(x => x.Name == "Visitor").FirstOrDefault();
            
            for(int i=0;i<builders.Length;i++){
                builders[i].WithDescription($"{roles[i].Mention}\n");
                builders[i].WithColor(roles[i].Color);
            }
            builders[0].Description += "Leader of the clan, person with all permissions, only warlord can ban from discord server";
            builders[1].Description += "Administrators of the clan they have almost all permissions, they can kick members out of the clan and construct new dojo rooms";
            builders[2].Description += "Moderators of the clan they can promote other members and mute in text channels too. If you think you should be promoted, you have trouble changing your nickname or you will be offline for a long time message one of them";
            builders[3].Description += "People that help maintain the clan, they can recruit new members and queue new research, can also mute members in voice channels. If you want someone added to the clan or you see new research to be done, messeage one of them";
            builders[4].Description += "Accepted member of the clan";
            builders[5].Description += "Friends of the clan that are not in the clan itself";
            builders[6].Description += "";
            builders[7].Description += "Newcommers on the server";
            foreach (EmbedBuilder builder in builders){
                await Context.Channel.SendMessageAsync("",false,builder);
                await Task.Delay(1000);
            }
        }
    }
}