using Handler.Core.Common;

namespace Handler.Core.Abstractions.Repositories;

public interface IStoreRepository
{
    public Task<(TimeSpan cookingTime, string? error)> GetCokingTime(Guid storeId, Product[] basket);
}