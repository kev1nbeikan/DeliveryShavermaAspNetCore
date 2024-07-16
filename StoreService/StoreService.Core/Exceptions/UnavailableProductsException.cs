using BarsGroupProjectN1.Core.Models.Payment;
using StoreService.Core.Abstractions;

namespace StoreService.Core.Exceptions;

public class UnavailableProductsException
    : StoreServiceException
{
    public UnavailableProductsException(List<ProductInventoryWithName> products) : base(
        $"Недостаточно продуктов в магазине: {string.Join(", ", products.Select(p => $"Продукт: {p.Name} в количестве: {p.Quantity} не доступен, свяжитесь с магаизном или поменяйте корзину"))}")
    {
    }

    public UnavailableProductsException(Guid productId, Guid storeId) : base(
        $"В магазине в айди {storeId} нет продукта {productId}")
    {
    }
}