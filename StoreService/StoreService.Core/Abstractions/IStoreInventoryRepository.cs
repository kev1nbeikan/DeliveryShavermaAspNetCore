using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreService.Core.Abstractions;

public interface IStoreInventoryRepository
{
    Task<List<ProductInventory>> GetByIds(Guid storeId, List<Guid> productsIds);
    Task Add(ProductInventory productInventory);
    Task<ProductInventory?> GetById(Guid storeId, Guid productId);
    Task<bool> UpdateQuantity(ProductInventory productInventory);
}