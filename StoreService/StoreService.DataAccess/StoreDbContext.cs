using Microsoft.EntityFrameworkCore;

namespace StoreService.DataAccess;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {
    }
}