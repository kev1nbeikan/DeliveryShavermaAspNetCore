using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Payment;
using BarsGroupProjectN1.Core.Models.Store;
using Handler.Core.Payment;

namespace Handler.Core.Abstractions.Services;

public interface IStoreService
{
    Task<(OrderTaskExecution<Store>? cookingExecution, string? error)> GetCookingTime(string address,
        List<ProductsInventory> basket);
}