using StoreService.Core;
using StoreService.Core.Abstractions;

namespace StoreService.Application;

public class StoreUseCases : IStoreUseCases
{
    private readonly IStoreService _storeService;
    private readonly IStoreServiceMenuRepository _menuRepository;

    public StoreUseCases(IStoreServiceMenuRepository menuRepository, IStoreService storeService)
    {
        _menuRepository = menuRepository;
        _storeService = storeService;
    }

    public Task<List<MenuProductInventory>> GetMenuProductsWithInventory(Guid storeId)
    {
        throw new NotImplementedException();
    }
}