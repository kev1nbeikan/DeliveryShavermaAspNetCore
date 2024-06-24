using System;

namespace StoreService.Core;

public class ProductQuantity
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }


    public ProductQuantity(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public ProductQuantity()
    {
    }

    public static ProductQuantity Create(Guid productId, int quantity)
    {
        if (productId == Guid.Empty) throw new ArgumentException(nameof(productId) + " cannot be empty");
        if (quantity < 0) throw new ArgumentException(nameof(quantity) + " cannot be less than 1");

        return new ProductQuantity { ProductId = productId, Quantity = quantity };
    }
}