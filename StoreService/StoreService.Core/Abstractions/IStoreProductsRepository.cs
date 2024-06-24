using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreService.Core.Abstractions;

public interface IStoreProductsRepository
{
    Task<List<ProductQuantity>> GetProductsQuantiies(Guid storeId, List<ProductQuantity> products);
}