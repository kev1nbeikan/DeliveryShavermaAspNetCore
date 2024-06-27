using Handler.Core;
using Handler.Core.Common;
using Handler.Core.Payment;

namespace HandlerService.Controllers;

public interface IStoreService
{
    Task<(TimeSpan cookingTime, string? error)> GetCookingTime(string address, List<BucketItem> basket);
}