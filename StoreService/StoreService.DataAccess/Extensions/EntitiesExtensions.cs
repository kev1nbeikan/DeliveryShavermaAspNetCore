using BarsGroupProjectN1.Core.Models.Store;
using StoreService.Core;
using StoreService.DataAccess.Entities;

namespace StoreService.DataAccess.Extensions;

public static class EntitiesExtensions
{
    public static ProductInventory ToCore(this ProductInventoryEntity productInventoryEntity)
    {
        return new ProductInventory
        {
            ProductId = productInventoryEntity.ProductId,
            StoreId = productInventoryEntity.StoreId,
            Quantity = productInventoryEntity.Quantity
        };
    }

    public static ProductInventoryEntity ToEntity(this ProductInventory productInventory)
    {
        return new ProductInventoryEntity
        {
            ProductId = productInventory.ProductId,
            StoreId = productInventory.StoreId,
            Quantity = productInventory.Quantity
        };
    }

    public static Store ToCore(this StoreEntity storeEntity)
    {
        return new Store
        {
            Id = storeEntity.Id,
            Status = storeEntity.Status,
            ActiveOrdersCount = storeEntity.ActiveOrdersCount
        };
    }

    public static StoreEntity ToEntity(this Store store)
    {
        return new StoreEntity
        {
            Id = store.Id,
            Status = store.Status,
            ActiveOrdersCount = store.ActiveOrdersCount
        };
    }
}