using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Common;
using Handler.Core.Payment;
using HandlerService.Controllers;

namespace HandlerService.Application.Services;

public class StoreService : IStoreService
{
    private readonly IStoreRepository _storeRepository;

    public StoreService(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<(TimeSpan cookingTime, string? error)> GetCookingTime(Guid storeId, List<ProductQuantity> productsAndQuantities)
    {
        return await _storeRepository.GetCokingTime(storeId, productsAndQuantities);
    }
}