using Handler.Core.Payment;

namespace Handler.Core.Abstractions.Repositories;

public interface IStoreRepository
{
    public Task<(TimeSpan cookingTime, string? error)> GetCokingTime(string clientAddress, List<BucketItem> basket);
}