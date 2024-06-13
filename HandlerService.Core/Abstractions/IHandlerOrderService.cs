namespace Handler.Core.Abstractions;

public interface IHandlerOrderService
{
    (Order? order, string? error) Save(Guid newGuid, Guid userId, Product[] products, long price,
        string paymentRequestAddress,
        string paymentRequestComment);

    Order? Get(Guid orderId);
}