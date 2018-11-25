using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace THONK.Resources.Database{
    /* create structure of guild record */
    public class Guild {
        /* primary key */
        [Key]
        public ulong GuildID {get;set;}
        public string Prefix {get;set;}
        public ulong ChannelGeneral {get;set;}
        public ulong ChannelAnnouncements {get;set;}
        public ulong ChannelBotLog {get;set;}
        public ulong ChannelLog {get;set;}
    }
}