using Handler.Core.Common;
using Handler.Core.HanlderService;
using Handler.Core.Payment;

namespace Handler.Core.Abstractions.Services;

public interface IHandlerOrderService
{
    (TemporyOrder? order, string? error) Save(Guid newGuid, Guid userId, Product[] products, int price,
        string address,
        string comment, List<BucketItem> productAndQuantity);

    TemporyOrder? Get(Guid orderId);
}