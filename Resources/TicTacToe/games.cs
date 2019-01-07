using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace THONK.Resources.TicTacToe{
    /* create structure of guild record */
    public class Game {
        /* primary key */
        [Key]
        public ulong userId {get;set;}
        public bool?[][] game {get;set;}
        public bool canMove {get;set;}
        public bool slowGame {get;set;}
    }
}