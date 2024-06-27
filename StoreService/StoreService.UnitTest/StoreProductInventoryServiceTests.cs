using StoreService.Application;
using StoreService.Core;
using StoreService.Core.Abstractions;
using StoreService.DataAccess.Repositories;
using StoreService.UnitTest.Fixtures;
using StoreService.UnitTest.Utils;

namespace StoreService.UnitTest;

public class StoreProductInventoryServiceTests
{
    private IStoreProductsService _storeProductsService;

    [SetUp]
    public void Setup()
    {
        _storeProductsService = new StoreProductService(
            new StoreInventoryRepository(DbContextFixture.Context)
        );
    }


    [Test]
    public async Task AddAndUpsertProductInventory()
    {
        var product = new ProductInventory
        {
            ProductId = Guid.NewGuid(),
            StoreId = Guid.NewGuid(),
            Quantity = 10
        };

        await _storeProductsService.UpsertProductInventory(
            storeId: product.StoreId,
            productId: product.ProductId,
            quantity: product.Quantity);


        var productFromRepo =
            await _storeProductsService.GetById(storeId: product.StoreId, productId: product.ProductId);

        Assert.That(productFromRepo, Is.Not.Null);
        CustomAsserts.AssertAreEqual(product, productFromRepo);
    }
}