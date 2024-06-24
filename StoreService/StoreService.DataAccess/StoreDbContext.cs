using Microsoft.EntityFrameworkCore;
using StoreService.DataAccess.Entities;

namespace StoreService.DataAccess;

public class StoreDbContext : DbContext
{
    public DbSet<ProductInventoryEntity> StoreProductsInventory { get; set; }

    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {
    }
}