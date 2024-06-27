using StoreService.Core;

namespace StoreService.Application;

public interface IStoreServiceSpecificMenuRepository
{
    Task<List<Product>> GetAll();
}