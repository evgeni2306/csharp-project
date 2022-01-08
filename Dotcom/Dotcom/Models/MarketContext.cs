using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Dotcom.Models
{
    public class MarketContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }

        public MarketContext(DbContextOptions<MarketContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
