using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Extensions;

namespace HandlerService.Application.Services;

public class HandlerOrderService : IHandlerOrderService
{
    public (HandlerServiceOrder? order, string? error) Save(Guid newGuid, Guid userId, Product[] products, int price,
        string address,
        string comment)
    {
        var (order, error) = HandlerServiceOrder.Create(
            newGuid,
            products,
            price,
            comment,
            address,
            userId,
            userId
        );

        if (!string.IsNullOrEmpty(error)) return (null, error);
        return (order, null);
    }


    public HandlerServiceOrder? Get(Guid orderId)
    {
        return HandlerServiceOrder.Create(orderId, new Product[0], 0, "", "", Guid.Empty, Guid.Empty).Order;
    }
}