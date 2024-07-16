using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OrderService.DataAccess;

namespace OrderService.UnitTests.Utils;

public class FakeDbContext(DbContextOptions options) : OrderServiceDbContext(options)
{
    public static FakeDbContext Create()
    {
        var contextOptions = new DbContextOptionsBuilder<OrderServiceDbContext>()
            .UseInMemoryDatabase("FakeDbContextTestOrderService")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        return new FakeDbContext(contextOptions);
    }
}