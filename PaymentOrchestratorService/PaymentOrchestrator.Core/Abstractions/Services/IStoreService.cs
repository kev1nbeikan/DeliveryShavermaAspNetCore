using Handler.Core;
using Handler.Core.Common;

namespace HandlerService.Controllers;

public interface IStoreService
{
    Task<(TimeSpan cookingTime, string? error)> GetCookingTime(Guid storeId, Product[] basket);
}