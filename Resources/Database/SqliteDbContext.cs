using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace THONK.Resources.Database{
    /* create sqlite context extending database context */
    public class SqliteDbContext : DbContext {
        /* initialize Guilds table */
        public DbSet<Guild> Guilds {get;set;}
        /* override database loading method to load given file */
        protected override void OnConfiguring(DbContextOptionsBuilder Options){
            Options.UseSqlite("Data Source=db.sqlite");
        }
    }
}