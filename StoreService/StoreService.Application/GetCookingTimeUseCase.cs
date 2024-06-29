using BarsGroupProjectN1.Core.Models.Payment;
using StoreService.Core;
using StoreService.Core.Abstractions;

namespace StoreService.Application;

public class GetCookingTimeUseCase : IGetCookingTimeUseCase
{
    private static readonly TimeSpan DefaultCookingTime = TimeSpan.FromMinutes(5);

    public TimeSpan GetCookingTime(Guid storeId, List<ProductsInventory> productsAndQuantities)
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