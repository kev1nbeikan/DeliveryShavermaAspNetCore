using Handler.Core.Abstractions.Repositories;
using Handler.Core.HanlderService;
using Microsoft.Extensions.Caching.Memory;

namespace HandlerService.DataAccess.Repositories;

public class HandlerRepository : IHandlerRepository
{
    private readonly IMemoryCache _cache;

    public HandlerRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public string? Save(PaymentOrder? order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }

        _cache.Set(order.Id.ToString(), order);

        return null;
    }

    public PaymentOrder? Get(Guid orderId)
    {
        return _cache.TryGetValue(orderId.ToString(), out PaymentOrder? order)
            ? order
            : null;
    }
}