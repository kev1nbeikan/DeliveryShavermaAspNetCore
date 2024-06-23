using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Configurations;
using OrderService.DataAccess.Entities;

namespace OrderService.DataAccess
{
    public class OrderServiceDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CurrentOrderConfiguration());
            modelBuilder.ApplyConfiguration(new CanceledOrderConfiguration());
            modelBuilder.ApplyConfiguration(new LastOrderConfiguration());
        }

        public DbSet<CurrentOrderEntity> CurrentOrders { get; set; }
        public DbSet<LastOrderEntity> LastOrders { get; set; }
        public DbSet<CanceledOrderEntity> CanceledOrders { get; set; }

    }
}
