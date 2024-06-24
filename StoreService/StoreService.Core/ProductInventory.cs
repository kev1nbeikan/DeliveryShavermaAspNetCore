using System;

namespace StoreService.Core;

public class ProductInventory
{
    public Guid ProductId { get; set; }
    public Guid StoreId { get; set; }
    public int Quantity { get; set; }


    public ProductInventory(Guid productId, int quantity, Guid storeId)
    {
        ProductId = productId;
        Quantity = quantity;
        StoreId = storeId;
    }

    public ProductInventory()
    {
    }

    public static ProductInventory Create(Guid productId, Guid storeId, int quantity)
    {
        if (productId == Guid.Empty) throw new ArgumentException(nameof(productId) + " cannot be empty");
        if (storeId == Guid.Empty) throw new ArgumentException(nameof(storeId) + " cannot be empty");
        if (quantity < 0) throw new ArgumentException(nameof(quantity) + " cannot be less than 1");

        return new ProductInventory { ProductId = productId, StoreId = storeId, Quantity = quantity };
    }
}