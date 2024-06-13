namespace Handler.Core.Abstractions;

public interface IHandlerOrderService
{
    (HandlerServiceOrder? order, string? error) Save(Guid newGuid, Guid userId, Product[] products, int price,
        string address,
        string comment);

    HandlerServiceOrder? Get(Guid orderId);
}