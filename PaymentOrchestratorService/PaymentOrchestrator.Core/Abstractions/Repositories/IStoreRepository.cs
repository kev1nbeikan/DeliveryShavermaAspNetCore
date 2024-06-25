using Handler.Core.Common;
using Handler.Core.Payment;

namespace Handler.Core.Abstractions.Repositories;

public interface IStoreRepository
{
    public Task<(TimeSpan cookingTime, string? error)> GetCokingTime(Guid storeId, List<BucketItem> basket);
}