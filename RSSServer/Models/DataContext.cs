using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RSSServer.Models
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { set; get; }
        public DbSet<Channel> Channels { set; get; }
        public DbSet<Collection> Collections { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=readerDB;Trusted_Connection=True;");
        }
    }
}