using System.Collections;
using BarsGroupProjectN1.Core.Models.Payment;
using StoreService.Core;
using StoreService.Core.Abstractions;

namespace StoreService.Application;

public class StoreProductService : IStoreProductsService
{
    private readonly IStoreInventoryRepository _storeInventoryRepository;

    public StoreProductService(IStoreInventoryRepository storeInventoryRepository)
    {
        _storeInventoryRepository = storeInventoryRepository;
    }

    public async Task<bool> CheckProductsCount(Guid storeId,
        List<ProductsInventoryWithoutStore> requiredProductsQuantities)
    {
        List<ProductInventory> availableProductsQuantities =
            await _storeInventoryRepository.GetByIds(storeId,
                requiredProductsQuantities.Select(p => p.ProductId).ToList());

        return CheckProductsCountCore(requiredProductsQuantities, availableProductsQuantities);
    }

    private static bool CheckProductsCountCore(
        List<ProductsInventoryWithoutStore> requiredProductsQuantities,
        List<ProductInventory> availableProductsQuantities
    )
    {
        foreach (var requiredProductQuantity in requiredProductsQuantities)
        {
            var availableProductQuantity =
                availableProductsQuantities.Find(x =>
                    x.ProductId == requiredProductQuantity.ProductId);

            if (availableProductQuantity!.Quantity < requiredProductQuantity.Quantity)
            {
                return false;
            }
        }

        return true;
    }
}