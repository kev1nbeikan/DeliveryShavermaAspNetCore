using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Payment;
using BarsGroupProjectN1.Core.Models.Store;
using StoreService.Core;
using StoreService.Core.Abstractions;
using StoreService.Core.Exceptions;

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

    public async Task<OrderTaskExecution<Store>> GetCookingInfo(
        string clientAddress,
        List<ProductsInventoryWithoutStore> products
    )
    {
        var store = await GetStoreForClientAddress(clientAddress);

        if (store is null) throw new StoreNotFoundException(clientAddress);

        await EnsureValidStoreAndProducts(store.Id, products);

        return new OrderTaskExecution<Store>
        {
            Executor = store,
            Time = _getCookingTimeUseCase.GetCookingTime(store.Id, products)
        };
    }

    private async Task<Store?> GetStoreForClientAddress(string clientAddress)
    {
        // TODO: Implement logic to find store near client address

        var stores = await _storeRepository.GetAll();

        if (stores.Count == 0) return null;

        var random = new Random();
        return stores.ElementAt(random.Next(stores.Count));
    }

    public async Task<StoreStatus> GetStatus(Guid storeId)
    {
        StoreStatus? status = await _storeRepository.GetStatus(storeId);

        if (status is null) throw new StoreNotFoundException(storeId);

        return (StoreStatus)status;
    }

    public async Task<Store> GetOrAddNewStore(Guid storeId)
    {
        var store = await _storeRepository.Get(storeId);
        if (store is not null) return store;
        store = Store.Create(storeId);
        await _storeRepository.Add(store);
        return store;
    }

    public async Task UpdateStatus(Guid storeId, StoreStatus status)
    {
        var store = await _storeRepository.Get(storeId);
        if (store is null) throw new StoreNotFoundException(storeId);

        var storeUpdated = Store.Create(storeId, status);
        await _storeRepository.Update(storeUpdated);
    }

    private async Task EnsureValidStoreAndProducts(Guid storeId, List<ProductsInventoryWithoutStore> products)
    {
        var store = await _storeRepository.Get(storeId);
        if (store is null) throw new StoreNotFoundException(storeId);

        if (store.Status != StoreStatus.Open)
            throw new StoreClosedException(storeId);

        if (!await _storeProductsService.CheckProductsCount(storeId, products))
            throw new UnavailableProductsException(products);
    }
}