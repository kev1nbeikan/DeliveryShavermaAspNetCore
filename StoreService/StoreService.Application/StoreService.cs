using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
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
        List<ProductsInventory> products
    )
    {
        var store = await GetStoreForClientAddress(clientAddress);

        if (store is null) throw new StoreNotFoundException(clientAddress);

        await EnsureValidStoreAndProductsToMakeOrder(store.Id, products);

        return new OrderTaskExecution<Store>
        {
            Executor = store,
            Time = _getCookingTimeUseCase.GetCookingTime(store.Id, products)
        };
    }

    private async Task<Store?> GetStoreForClientAddress(string clientAddress)
    {
        // TODO: Implement logic to find store near client address

        var stores = await _storeRepository.GetAllOpened();

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

    public async Task<bool> UpdateStore(Guid storeId, string address)
    {
        var store = await _storeRepository.Get(storeId);
        if (store is null) throw new StoreNotFoundException(storeId);

        store.Address = address;
        store.EnsureIsValid();

        return await _storeRepository.Update(store);
    }

    
    public async Task UpdateStatus(Guid storeId, StoreStatus newStatus)
    {
        var store = await _storeRepository.Get(storeId);
        if (store is null) throw new StoreNotFoundException(storeId);

        EnsureValidChangingOfStatus(newStatus, store);

        store.Status = newStatus;

        await _storeRepository.Update(store);
    }

    private void EnsureValidChangingOfStatus(StoreStatus newStatus, Store store)
    {
        switch (newStatus)
        {
            case StoreStatus.Open:
                EnsureValidStoreToOpen(store);
                break;
            case StoreStatus.Closed:
                EnsureValidStoreToClose(store);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus,
                    "Вы пытаетесь установить неизвестный статус магазина.");
        }
    }

    private void EnsureValidStoreToClose(Store store)
    {
        if (store.ActiveOrdersCount > 0)
        {
            throw new ArgumentException(
                "Магазин не может быть открыт, если имеются активные заказы. Если таковых нет, то обратитесь к администратору");
        }
    }

    public async Task OnOrderCreate(PublishOrder order)
    {
        await AdjustActiveOrdersCount(order.StoreId, adjustment: 1);
    }

    public async Task OnOrderUpdate(PublishOrder order)
    {
        await AdjustActiveOrdersCount(order.StoreId, adjustment: -1);
    }

    public async Task AdjustActiveOrdersCount(Guid storeId, int adjustment = 1)
    {
        var store = await _storeRepository.Get(storeId);
        if (store is null) throw new StoreNotFoundException(storeId);

        store.ActiveOrdersCount += adjustment;

        store.EnsureIsValid();

        await _storeRepository.Update(store);
    }

    private async Task EnsureValidStoreAndProductsToMakeOrder(Guid storeId, List<ProductsInventory> products)
    {
        var store = await _storeRepository.Get(storeId);
        if (store is null) throw new StoreNotFoundException(storeId);
        store.EnsureIsValid();

        if (store!.Status != StoreStatus.Open)
            throw new StoreClosedException(storeId);

        if (!await _storeProductsService.CheckProductsCount(storeId, products))
            throw new UnavailableProductsException(products);
    }

    private void EnsureValidStoreToOpen(Store store)
    {
        store.EnsureIsValid();
    }
}