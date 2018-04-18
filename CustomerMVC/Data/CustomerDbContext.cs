using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerMVC.Data
{
    public class CustomerDbContext : DbContext
    {
        // Database tables EFC wires up for us; command-line tools.
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
  // The .NET EF commandline tools handle the process of matching tables and columns via migrations e.g. dotnet ef migrations add WeBrokeItWeFixedIt


        
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {

        }

        // Make sure OrderProducts have two primary keys; Order ID and Product ID
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OrderProduct>()
              .HasKey(oP => new { oP.orderID, oP.productID });
        }
    }
}
