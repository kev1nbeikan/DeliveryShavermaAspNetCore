using StoreService.Core;

namespace StoreService.Application;

public interface IStoreServiceMenuRepository
{
    Task<List<Product>> GetAll();
}