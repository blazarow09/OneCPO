using Microsoft.EntityFrameworkCore;
using OneCPO.Data.Models;

namespace OneCPO.Data
{
    public class OneCPODbContext : DbContext
    {
        public OneCPODbContext(DbContextOptions<OneCPODbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}