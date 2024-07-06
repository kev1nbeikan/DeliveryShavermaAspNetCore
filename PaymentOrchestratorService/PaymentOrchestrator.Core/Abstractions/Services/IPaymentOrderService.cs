using Handler.Core.Common;
using Handler.Core.HanlderService;
using Handler.Core.Payment;

namespace Handler.Core.Abstractions.Services;

public interface IPaymentOrderService
{
    (PaymentOrder? order, string? error) Save(Guid newGuid, Guid userId, Product[] products, int price,
        string address,
        string comment, List<(Product product, int amount, int price)> productAndQuantity, string clientNumber);

    PaymentOrder? Get(Guid orderId);
}