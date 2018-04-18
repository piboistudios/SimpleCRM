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

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }




        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //  modelBuilder.Entity<Note>()
            //    .HasOne(c => c.customer)
            //  .WithMany(n => n.notes);
            modelBuilder.Entity<OrderProduct>()
              .HasKey(oP => new { oP.orderID, oP.productID });
        }
    }
}
