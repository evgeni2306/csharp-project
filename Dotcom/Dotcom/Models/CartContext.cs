using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotcom.Models
{
    public class CartContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }

        public CartContext(DbContextOptions<CartContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
