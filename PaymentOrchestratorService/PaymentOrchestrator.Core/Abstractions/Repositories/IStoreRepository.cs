using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Payment;
using BarsGroupProjectN1.Core.Models.Store;
using Handler.Core.Payment;

namespace Handler.Core.Abstractions.Repositories;

public interface IStoreRepository
{
    public Task<(OrderTaskExecution<Store>? cookingExecution, string? error)> GetCokingTime(string clientAddress,
        List<ProductInventoryWithName> basket);
}