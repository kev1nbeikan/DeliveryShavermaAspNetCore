using StoreService.Core;
using StoreService.Core.Abstractions;
using StoreService.Core.Exceptions;
using StoreService.Main.Controllers;

namespace StoreService.Application;

public class StoreService : IStoreService
{
    private readonly IStoreRepository _storeRepository;
    private readonly ICookingTimerService _cookingTimerService;
    private readonly IStoreProductsService _storeProductsService;

    public StoreService(IStoreRepository storeRepository, ICookingTimerService cookingTimerService, IStoreProductsService storeProductsService)
    {
        _storeRepository = storeRepository;
        _cookingTimerService = cookingTimerService;
        _storeProductsService = storeProductsService;
    }

    public async Task<TimeSpan> GetCookingTime(Guid storeId, List<ProductQuantity> products)
    {
        var store = await _storeRepository.GetStore(storeId);
        if (store.Status != StoreStatus.Open)
            throw new NotFoundException<Guid>("store is closed", storeId);

        if (!await _storeProductsService.CheckProductsCount(storeId, products))
        {
            throw new NotFoundException<List<ProductQuantity>>("not enough products in store", products);
        }

        TimeSpan time = _cookingTimerService.GetCookingTime(storeId, products);
        return time;
    }
}