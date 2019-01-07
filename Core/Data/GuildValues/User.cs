using System;
using System.Linq;
using Discord;
using Discord.WebSocket;

namespace THONK.Core.Data.GuildValues{
    public class User{
        public static bool HasHigherRole(SocketGuildUser user, string LowestRole){
            var UserRoles = user.Roles.ToArray();
            foreach (var role in UserRoles){
                if(RoleToInt(role.Name)<=RoleToInt(LowestRole)){
                    return true;
                }
            }
            return false;
        }
        static int RoleToInt(string role){
            switch(role){
                case "Warlord":
                    return 0;
                case "General":
                    return 1;
                case "Lieutenant":
                    return 2;
                case "Sergeant":
                    return 3;
                case "Soldier":
                    return 4;
                case "Guest":
                    return 5;
                case "Initiate":
                    return 6;
                case "Visitor":
                    return 7;
                default:
                    return 8;
            }
        }
    }
}