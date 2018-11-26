using System;
using System.Linq;
using Discord;
using Discord.WebSocket;

namespace THONK.Core.Data.GuildValues{
    public class User{
        public static bool HasRole(IGuildUser user, string LowestRole){
            var roles = user.Guild.Roles.ToArray();
            bool Allowed = false;
            foreach (var role in roles){
                switch(role.Name){
                    case "Warlord":
                        if(role.Name==LowestRole){
                            Allowed = true;
                        }
                        break;
                    case "General":
                        if(role.Name==LowestRole){
                            Allowed = true;
                        }break;
                    case "Lieutenant":
                        if(role.Name==LowestRole){
                            Allowed = true;
                        }break;
                    case "Sergeant":
                        if(role.Name==LowestRole){
                            Allowed = true;
                        }break;
                    case "Soldier":
                        if(role.Name==LowestRole){
                            Allowed = true;
                        }break;
                    case "Initiate":
                        if(role.Name==LowestRole){
                            Allowed = true;
                        }break;
                    case "Guest":
                        if(role.Name==LowestRole){
                            Allowed = true;
                        }break;
                    default:
                        break;
                }
            }
            return Allowed;
        }
    }
}