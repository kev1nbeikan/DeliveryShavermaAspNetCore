using StoreService.Core;
using StoreService.Core.Abstractions;

namespace StoreService.Application;

public class StoreUseCases : IStoreUseCases
{
    private readonly IStoreService _storeService;
    private readonly IMenuRepositoryApi _menuRepositoryApi;

    public StoreUseCases(IMenuRepositoryApi menuRepositoryApi, IStoreService storeService)
    {
        _menuRepositoryApi = menuRepositoryApi;
        _storeService = storeService;
    }

    public Task<List<MenuProductInventory>> GetMenuProductsWithInventory(Guid storeId)
    {
        throw new NotImplementedException();
    }
}