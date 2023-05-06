using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using System.Reflection.Metadata;

namespace DataLayer.Data
{
    public class MarketApiContext : DbContext
    {
        public MarketApiContext (DbContextOptions<MarketApiContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; } = default!;

        public DbSet<Item> Item { get; set; }

        public DbSet<OrderedItem> OrderedItem { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Receipt> Receipt { get; set; }

        //public DbSet<MarketApi.Models.OrderedItem> OrderedItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Order>()
            //    .HasOne<Customer>(e => e.Customer)
            //    .WithMany(r => r.Orders)
            //    .HasForeignKey(e => e.CustomerId);
            modelBuilder.Entity<OrderedItem>()
                .HasKey(pc => new { pc.OrderId, pc.ItemId });

            modelBuilder.Entity<OrderedItem>()
                .HasOne(p => p.Order)
                .WithMany(pc => pc.OrderedItems)
                .HasForeignKey(pc => pc.OrderId);

            modelBuilder.Entity<OrderedItem>()
                .HasOne(p => p.Item)
                .WithMany(pc => pc.OrderedItems)
                .HasForeignKey(p => p.ItemId);

        }
    }
}
