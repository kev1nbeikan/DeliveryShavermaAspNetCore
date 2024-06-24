using Handler.Core.Common;
using Handler.Core.HanlderService;
using Handler.Core.Payment;

namespace Handler.Core.Abstractions.Services;

public interface IHandlerOrderService
{
    (TemporyOrder? order, string? error) Save(Guid newGuid, Guid userId, Product[] products,
        List<ProductQuantity> productIdsAndQuantity, int price,
        string address,
        string comment);

    TemporyOrder? Get(Guid orderId);
}