using StoreService.Core;
using StoreService.Core.Abstractions;
using StoreService.DataAccess.Repositories;
using StoreService.UnitTest.Fixtures;
using StoreService.UnitTest.Utils;

namespace StoreService.UnitTest;

public class StoreInventoryRepositoryTests
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
        CustomAsserts.AssertAreEqual(product, productFromRepo);
    }

    [Test]
    public async Task AddAndUpdate()
    {
        var product = new ProductInventory
        {
            ProductId = Guid.NewGuid(),
            StoreId = Guid.NewGuid(),
            Quantity = 10
        };

        await _storeInventoryRepository.Add(product);

        product.Quantity = 20;
        var updateResult = await _storeInventoryRepository.UpdateQuantity(product);
        var productFromRepo = await _storeInventoryRepository.GetById(product.StoreId, product.ProductId);
        
        Assert.That(updateResult, Is.True);
        Assert.That(productFromRepo, Is.Not.Null);
        CustomAsserts.AssertAreEqual(product, productFromRepo);
    }
}