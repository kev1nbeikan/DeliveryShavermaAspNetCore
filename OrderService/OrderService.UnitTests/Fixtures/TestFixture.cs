using NUnit.Framework;
using OrderService.DataAccess;
using OrderService.UnitTests.Utils;

namespace OrderService.UnitTests.Fixtures;

[SetUpFixture]
public static class TestFixture
{
    public static OrderServiceDbContext OrderServiceDbContext { get; } =  FakeDbContext.Create();
    
    [OneTimeSetUp]
    public static async Task BaseSetUp()
    {
        Console.WriteLine($"Используется строка подключения тест");
    }
    
    [OneTimeTearDown]
    public static  async Task BaseTearDown()
    {
        await OrderServiceDbContext.DisposeAsync();
    }
}