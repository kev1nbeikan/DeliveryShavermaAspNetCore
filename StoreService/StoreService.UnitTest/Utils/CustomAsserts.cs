using StoreService.Core;

namespace StoreService.UnitTest.Utils;

public abstract class CustomAsserts
{
    public static void AssertAreEqual(ProductInventory expected, ProductInventory actual)
    {
        Assert.That(actual.ProductId, Is.EqualTo(expected.ProductId));
        Assert.That(actual.StoreId, Is.EqualTo(expected.StoreId));
        Assert.That(actual.Quantity, Is.EqualTo(expected.Quantity));
    }

    public static void AssertAreEqual(Store expected, Store actual)
    {
        Assert.That(actual.Id, Is.EqualTo(expected.Id));
        Assert.That(actual.Status, Is.EqualTo(expected.Status));
    }
}