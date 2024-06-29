using Handler.Core.Common;
using Handler.Core.HanlderService;
using Handler.Core.Payment;

namespace Handler.Core.Abstractions.UseCases;

public interface IPaymentUseCases
{
    public Task<(Product[] products, int price, PaymentOrder? paymentOrder)> ExecutePaymentBuild(
        List<ProductWithAmount> productIdsAndQuantity,
        string comment,
        string address,
        string phoneNumber,
        Guid userId);
}