using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Store;
using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Common;
using Handler.Core.Payment;

namespace HandlerService.Application.Services;

public class StoreService : IStoreService
{
    private readonly IStoreRepository _storeRepository;

    public StoreService(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<(OrderTaskExecution<Store>? cookingExecution, string? error)> GetCookingTime(string address,
        List<BucketItem> basket)
    {
        return await _storeRepository.GetCokingTime(address, basket);
    }
}