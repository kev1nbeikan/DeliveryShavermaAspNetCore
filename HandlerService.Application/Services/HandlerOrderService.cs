
using Handler.Core;
using Handler.Core.Abstractions;

namespace HandlerService.Application.Services;

public class HandlerOrderService : IHandlerOrderService
{
    public (Order? order, string? error) Save(Guid newGuid, Guid userId, Product[] products, long price,
        string paymentRequestAddress,
        string paymentRequestComment)
    {
        var (order, error) =
            Order.Create(newGuid, userId, products, price, paymentRequestAddress, paymentRequestComment);
        if (!string.IsNullOrEmpty(error)) return (null, error);
        return (order, null);
    }
}