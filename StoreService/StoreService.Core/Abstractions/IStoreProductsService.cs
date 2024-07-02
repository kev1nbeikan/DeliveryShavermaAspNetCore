using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarsGroupProjectN1.Core.Models.Payment;

namespace StoreService.Core.Abstractions;

public interface IStoreProductsService
{
    Task<bool> CheckProductsCount(Guid storeId, List<ProductsInventory> requiredProductsQuantities);
    Task UpsertProductInventory(Guid storeId, Guid productId, int quantity);
    Task<ProductInventory> GetById(Guid storeId, Guid productId);
    Task<List<ProductInventory>> GetAll(Guid storeId);
}