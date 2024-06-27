using BarsGroupProjectN1.Core.Models.Payment;
using StoreService.Core.Abstractions;

namespace StoreService.Core.Exceptions;

public class UnavailableProductsException
    : StoreServiceException
{
    public UnavailableProductsException(List<ProductsInventoryWithoutStore> products) : base(
        $"Not enough products in store: {string.Join(", ", products.Select(p => $"productId: {p.ProductId} quantity: {p.Quantity}"))}")
    {
    }

    public UnavailableProductsException(Guid productId, Guid storeId) : base(
        $"В магазине в айди {storeId} нет продукта {productId}")
    {
    }
}