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

    public StoreService(IStoreRepository storeRepository, IGetCookingTimeUseCase getCookingTimeUseCase,
        IStoreProductsService storeProductsService)
    {
        _storeRepository = storeRepository;
        _getCookingTimeUseCase = getCookingTimeUseCase;
        _storeProductsService = storeProductsService;
    }

    public async Task<TimeSpan> GetCookingTime(Guid storeId, List<ProductInventory> products)
    {
        await EnsureValidStoreAndProducts(storeId, products);

        return _getCookingTimeUseCase.GetCookingTime(storeId, products);
    }

    private async Task EnsureValidStoreAndProducts(Guid storeId, List<ProductInventory> products)
    {
        var store = await _storeRepository.Get(storeId);
        if (store is null) throw new StoreNotFoundException(storeId);
        if (store.Status != StoreStatus.Open)
            throw new StoreClosedException(storeId);
        if (!await _storeProductsService.CheckProductsCount(storeId, products))
            throw new UnavailableProductsException(products);
    }
}