using System;
using System.Threading.Tasks;

namespace StoreService.Core.Abstractions;

public interface IStoreRepository
{
    Task<Store> GetStore(Guid storeId);
}