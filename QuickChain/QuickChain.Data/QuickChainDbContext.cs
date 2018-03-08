using Microsoft.EntityFrameworkCore;
using QuickChain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickChain.Data
{
    public class QuickChainDbContext : DbContext
    {
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Peer> Peers { get; set; }

        public QuickChainDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO
        }
    }
}
