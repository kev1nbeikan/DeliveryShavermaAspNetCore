using StoreService.Core;

namespace StoreService.UnitTest.Extensions;

public abstract class ProductAssert
{
    public static void AssertAreEqual(ProductInventory expected, ProductInventory actual)
    {
        Assert.That(actual.ProductId, Is.EqualTo(expected.ProductId));
        Assert.That(actual.StoreId, Is.EqualTo(expected.StoreId));
        Assert.That(actual.Quantity, Is.EqualTo(expected.Quantity));
    }
}