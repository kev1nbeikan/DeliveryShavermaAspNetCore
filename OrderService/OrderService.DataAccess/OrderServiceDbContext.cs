using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Entities;

namespace OrderService.DataAccess
{
    public class OrderServiceDbContext : DbContext
    {
        public OrderServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public DbSet<CurrentOrderEntity> CurrentOrders { get; set; }
        public DbSet<LastOrderEnity> LastOrders { get; set; }
        public DbSet<CanceledOrderEntity> CanceledOrders { get; set; }

    }
}
