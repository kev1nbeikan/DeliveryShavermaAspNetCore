using Handler.Core.HanlderService;

namespace Handler.Core.Abstractions;

public interface IHandlerOrderService
{
    (PaymentOrder? order, string? error) Save(Guid newGuid, Guid userId, Product[] products, int price,
        string address,
        string comment);

    PaymentOrder? Get(Guid orderId);
}