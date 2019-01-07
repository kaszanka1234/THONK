using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace THONK.Resources.TicTacToe{
    /* create sqlite context extending database context */
    public class TicContext : DbContext {
        /* initialize Games table */
        public DbSet<Game> Games {get;set;}
    }
}