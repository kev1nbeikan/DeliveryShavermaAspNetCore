using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreService.Core.Abstractions;

public interface IStoreProductsService
{
    Task<bool> CheckProductsCount(Guid storeId, List<ProductInventory> requiredProductsQuantities);
}