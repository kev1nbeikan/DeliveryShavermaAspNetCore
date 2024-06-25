using Microsoft.EntityFrameworkCore;
using StoreService.DataAccess;

namespace StoreService.UnitTest.Utils;

public class FakeDbContext(DbContextOptions<StoreDbContext> options) : StoreDbContext(options)
{
    public static FakeDbContext? Create(string connectionString)
    {
        var contextOptions = new DbContextOptionsBuilder<StoreDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new FakeDbContext(contextOptions);
    }

    public void SetupData()
    {
    }
}