using StoreService.Core;
using StoreService.Core.Abstractions;

namespace StoreService.Application;

public class GetCookingTimeUseCase : IGetCookingTimeUseCase
{
    private static readonly TimeSpan DefaultCookingTime = TimeSpan.FromMinutes(5);

    public TimeSpan GetCookingTime(Guid storeId, List<ProductInventory> productsAndQuantities)
    {
        TimeSpan time = TimeSpan.Zero;
        foreach (var productAndQuantity in productsAndQuantities)
        {
            time += CalculateCookingTimeForProduct(productAndQuantity);
        }

        return time;
    }

    private TimeSpan CalculateCookingTimeForProduct(ProductInventory product)
    {
        return DefaultCookingTime * product.Quantity;
    }
}