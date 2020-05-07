using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RodBrosEntertainment.Models
{
    /// <summary>
    /// This provides the interface between the C# code and models, and the database.
    /// </summary>
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderProduct>().ToTable("OrderProducts");
        }
    }
}
