using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarsGroupProjectN1.Core.Models.Payment;

namespace StoreService.Core.Abstractions;

public interface IStoreProductsService
{
    Task<bool> CheckProductsCount(Guid storeId, List<ProductsInventoryWithoutStore> requiredProductsQuantities);
}