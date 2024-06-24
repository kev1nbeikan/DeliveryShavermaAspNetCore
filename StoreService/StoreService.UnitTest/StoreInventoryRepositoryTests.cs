using StoreService.Core;
using StoreService.Core.Abstractions;
using StoreService.DataAccess.Repositories;
using StoreService.UnitTest.Extensions;
using StoreService.UnitTest.Fixtures;

namespace StoreService.UnitTest;

public class Tests
{
    private IStoreInventoryRepository _storeInventoryRepository;

    [SetUp]
    public void Setup()
    {
        _storeInventoryRepository = new StoreInventoryRepository(DbContextFixture.Context);
    }

    [Test]
    public async Task AddAndGet()
    {
        var product = new ProductInventory
        {
            ProductId = Guid.NewGuid(),
            StoreId = Guid.NewGuid(),
            Quantity = 10
        };

        await _storeInventoryRepository.Add(product);

        var productFromRepo = await _storeInventoryRepository.GetById(product.StoreId, product.ProductId);
        Assert.That(productFromRepo, Is.Not.Null);
        ProductAssert.AssertAreEqual(product, productFromRepo);
    }
}