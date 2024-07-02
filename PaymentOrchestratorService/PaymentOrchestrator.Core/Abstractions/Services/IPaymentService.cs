using Handler.Core.Common;
using Handler.Core.HanlderService;
using Handler.Core.Payment;

namespace Handler.Core.Abstractions.Services;

public interface IPaymentService
{
    int CalculatePayment(Product[] products, List<ProductWithAmount> productIdsAndQuantity);
    List<PaymentType> GetPaymentTypes();

    (string? error, string? cheque) ConfirmPayment(PaymentOrder order, Payment paymentInfo);
}

