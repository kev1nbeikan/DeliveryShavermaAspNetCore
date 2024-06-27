using StoreService.Core;
using StoreService.Core.Abstractions;

namespace StoreService.Application;

public class StoreUseCases : IStoreUseCases
{
    private readonly IStoreService _storeService;
    private readonly IStoreServiceSpecificMenuRepository _menuRepository;

    public StoreUseCases(IMenuRepositoryByStoreService menuRepository, IStoreService storeService)
    {
        _menuRepository = menuRepository;
        _storeService = storeService;
    }

    public Task<List<MenuProductInventory>> GetMenuProductsWithInventory(Guid storeId)
    {
    }
}