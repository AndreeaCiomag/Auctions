using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.Models;

namespace MyApi.Models
{
    public class ItemContext : DbContext 
    {
        public ItemContext(DbContextOptions<ItemContext> options)
            :base(options)
        {

        }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}
