using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using UserService.DataAccess;

namespace UserService.UnitTests.Utils;

public class FakeDbContext(DbContextOptions<UserDbContext> options) : UserDbContext(options)
{
    public static FakeDbContext? Create(string connectionString)
    {
        var contextOptions = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase("FakeDbContextTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        return new FakeDbContext(contextOptions);
    }
    
    public void SetupData()
    {
        
        
    }
    
}