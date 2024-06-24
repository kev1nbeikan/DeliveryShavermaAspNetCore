using StoreService.Core;
using StoreService.Core.Abstractions;

namespace StoreService.Application;

public class StoreProductService : IStoreProductsService
{
    private readonly IStoreProductsRepository _storeProductsRepository;

    public StoreProductService(IStoreProductsRepository storeProductsRepository)
    {
        _storeProductsRepository = storeProductsRepository;
    }

    public async Task<bool> CheckProductsCount(Guid storeId, List<ProductQuantity> requiredProductsQuantities)
    {
        List<ProductQuantity> availableProductsQuantities =
            await _storeProductsRepository.GetProductsQuantiies(storeId, requiredProductsQuantities);

        foreach (var requiredProductQuantity in requiredProductsQuantities)
        {
            var availableProductQuantity =
                availableProductsQuantities.Find(x => x.ProductId == requiredProductQuantity.ProductId);

            if (availableProductQuantity!.Quantity < requiredProductQuantity.Quantity)
            {
                return false;
            }
        }

        return true;
    }
}