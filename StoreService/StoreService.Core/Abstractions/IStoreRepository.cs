using System;
using System.Threading.Tasks;

namespace StoreService.Core.Abstractions;

public interface IStoreRepository
{
    Task Add(Store store);

    Task<Store?> Get(Guid storeId);

    Task<bool> Update(Store store);
    Task<StoreStatus?> GetStatus(Guid storeId);
    Task<List<Store>> GetAll();
}