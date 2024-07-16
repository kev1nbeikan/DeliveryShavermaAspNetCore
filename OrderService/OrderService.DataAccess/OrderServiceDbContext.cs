using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Configurations;
using OrderService.DataAccess.Entities;

namespace OrderService.DataAccess
{
    
    /// <summary>
    /// Контекст взаимодействия с базой данных заказов.
    /// </summary>
    /// <param name="options"></param>
    public class OrderServiceDbContext(DbContextOptions options) : DbContext(options)
    {
        /// <inheritdoc />
        /// <remarks>Конфигурации для текущих, прошлых и отмененных заказов.</remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CurrentOrderConfiguration());
            modelBuilder.ApplyConfiguration(new CanceledOrderConfiguration());
            modelBuilder.ApplyConfiguration(new LastOrderConfiguration());
        }
        /// <summary>
        /// DbSet для операций над текущими заказами.
        /// </summary>
        public DbSet<CurrentOrderEntity> CurrentOrders { get; set; }
        /// <summary>
        /// DbSet для операций над прошлыми заказами.
        /// </summary>
        public DbSet<LastOrderEntity> LastOrders { get; set; }
        /// <summary>
        /// DbSet для операций над отмененными заказами.
        /// </summary>
        public DbSet<CanceledOrderEntity> CanceledOrders { get; set; }
    }
}