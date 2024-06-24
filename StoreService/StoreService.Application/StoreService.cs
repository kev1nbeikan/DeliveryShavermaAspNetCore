using StoreService.Core;
using StoreService.Core.Abstractions;
using StoreService.Core.Exceptions;
using StoreService.Main.Controllers;

namespace StoreService.Application;

public class StoreService : IStoreService
{
    private readonly IStoreRepository _storeRepository;
    private readonly IGetCookingTimeUseCase _getCookingTimeUseCase;
    private readonly IStoreProductsService _storeProductsService;

    public StoreService(IStoreRepository storeRepository, IGetCookingTimeUseCase getCookingTimeUseCase, IStoreProductsService storeProductsService)
    {
        _storeRepository = storeRepository;
        _getCookingTimeUseCase = getCookingTimeUseCase;
        _storeProductsService = storeProductsService;
    }

    public async Task<TimeSpan> GetCookingTime(Guid storeId, List<ProductInventory> products)
    {
        var store = await _storeRepository.Get(storeId);
        if (store.Status != StoreStatus.Open)
            throw new NotFoundException<Guid>("store is closed", storeId);

        if (!await _storeProductsService.CheckProductsCount(storeId, products))
        {
            throw new NotFoundException<List<ProductInventory>>("not enough products in store", products);
        }

        TimeSpan time = _getCookingTimeUseCase.GetCookingTime(storeId, products);
        return time;
    }
}