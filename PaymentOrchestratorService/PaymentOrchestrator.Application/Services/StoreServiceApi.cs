using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Store;
using BarsGroupProjectN1.Core.Models.Payment;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;

namespace HandlerService.Application.Services;

public class StoreServiceApi : IStoreService
{
    private readonly IStoreRepository _storeRepository;

    public StoreServiceApi(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<(OrderTaskExecution<Store>? cookingExecution, string? error)> GetCookingExecution(
        string clientAddress,
        List<ProductInventoryWithName> basket)
    {
        return await _storeRepository.GetCokingTime(clientAddress, basket);
    }
}