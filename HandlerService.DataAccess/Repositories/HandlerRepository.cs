using Handler.Core.Abstractions.Repositories;
using Handler.Core.HanlderService;
using Microsoft.Extensions.Caching.Memory;

namespace HandlerService.DataAccess.Repositories;

public class HandlerRepository : IHandlerRepository
{
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _entryMemoryCacheOptions;


    public HandlerRepository(IMemoryCache cache)
    {
        _cache = cache;
        _entryMemoryCacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(15));
    }

    public string? Save(PaymentOrder? order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }

        _cache.Set(order.Id.ToString(), order, _entryMemoryCacheOptions);

        return null;
    }

    public PaymentOrder? Get(Guid orderId)
    {
        return _cache.TryGetValue(orderId.ToString(), out PaymentOrder? order)
            ? order
            : null;
    }

    public string? Delete(Guid orderId)
    {
        _cache.Remove(orderId.ToString());
        return null;
    }
}