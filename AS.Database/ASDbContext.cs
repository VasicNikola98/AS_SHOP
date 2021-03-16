using ASonline.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Database
{
    public class ASDbContext : DbContext
    {
        public ASDbContext() : base("ASonlineConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<ProductImages> ProductImages { get; set; }

    }
}
