using AuthService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AuthService.UnitTests.Utils;

public class FakeDbContext(DbContextOptions<AuthDbContext> options) : AuthDbContext(options)
{
    public static FakeDbContext? Create(string connectionString)
    {
        var contextOptions = new DbContextOptionsBuilder<AuthDbContext>()
            .UseInMemoryDatabase("FakeDbContextTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        return new FakeDbContext(contextOptions);
    }
    
    public void SetupData()
    {
        
        
    }
    
}