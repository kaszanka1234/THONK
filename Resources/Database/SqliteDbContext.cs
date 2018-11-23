using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace THONK.Resources.Database{
    public class SqliteDbContext : DbContext {
        public DbSet<Guild> Guilds {get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder Options){
            Options.UseSqlite("Data Source=db.sqlite");
        }
    }
}