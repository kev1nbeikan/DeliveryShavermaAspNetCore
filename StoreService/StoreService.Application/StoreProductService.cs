using System.Collections;
using BarsGroupProjectN1.Core.Models.Payment;
using StoreService.Core;
using StoreService.Core.Abstractions;
using StoreService.Core.Exceptions;

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

    public async Task UpsertProductInventory(Guid storeId, Guid productId, int quantity)
    {
        var productInventory = ProductInventory.Create(productId: productId, storeId: storeId, quantity: quantity);

        var product = await _storeInventoryRepository.GetById(productInventory.StoreId, productInventory.ProductId);

        if (product is null)
        {
            await _storeInventoryRepository.Add(productInventory);
        }
        else
        {
            await _storeInventoryRepository.UpdateQuantity(productInventory);
        }
    }

    public async Task<ProductInventory> GetById(Guid storeId, Guid productId)
    {
        var productInventory = await _storeInventoryRepository.GetById(storeId, productId);

        if (productInventory is null) throw new UnavailableProductsException(storeId, productId);

        return productInventory;
    }

    public async Task<List<ProductInventory>> GetAll(Guid storeId)
    {
        return await _storeInventoryRepository.GetAll(storeId);
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