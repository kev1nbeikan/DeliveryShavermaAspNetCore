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
        List<ProductInventoryWithName> requiredProductsQuantities)
    {
        List<ProductInventory> availableProductsQuantities =
            await _storeInventoryRepository.GetByIds(storeId,
                requiredProductsQuantities.Select(p => p.ProductId).ToList());

        return CheckProductAvailability(requiredProductsQuantities, availableProductsQuantities);
    }

    private bool CheckProductAvailability(
        List<ProductInventoryWithName> requiredProductQuantities,
        List<ProductInventory> availableProductInventories
    )
    {
        if (requiredProductQuantities.Count != availableProductInventories.Count)
        {
            return false;
        }

        foreach (var requiredProductQuantity in requiredProductQuantities)
        {
            var availableProductInventory = availableProductInventories.Find(
                x => x.ProductId == requiredProductQuantity.ProductId
            );

            if (availableProductInventory is null) return false;

            if (availableProductInventory.Quantity < requiredProductQuantity.Quantity)
                return false;
        }

        return true;
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
}