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
}