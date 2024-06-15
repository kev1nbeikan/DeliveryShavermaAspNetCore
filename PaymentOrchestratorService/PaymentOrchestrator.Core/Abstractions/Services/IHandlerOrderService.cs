using Handler.Core.HanlderService;

namespace Handler.Core.Abstractions.Services;

public interface IHandlerOrderService
{
    (TemporyOrder? order, string? error) Save(Guid newGuid, Guid userId, Product[] products, int price,
        string address,
        string comment);

    TemporyOrder? Get(Guid orderId);
}