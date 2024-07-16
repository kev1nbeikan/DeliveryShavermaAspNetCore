using BarsGroupProjectN1.Core.Models.Payment;
using StoreService.Core;
using StoreService.Core.Abstractions;

namespace StoreService.Application;

public class GetCookingTimeUseCase : IGetCookingTimeUseCase
{
    private static readonly TimeSpan DefaultCookingTime = TimeSpan.FromMinutes(1);

    public TimeSpan GetCookingTime(Guid storeId, List<ProductInventoryWithName> productsAndQuantities)
    {
        TimeSpan time = TimeSpan.Zero;
        foreach (var productAndQuantity in productsAndQuantities)
        {
            time += CalculateCookingTimeForProduct(productAndQuantity);
        }

        return time;
    }

    private TimeSpan CalculateCookingTimeForProduct(ProductsInventory product)
    {
        return DefaultCookingTime * product.Quantity;
    }
}