using Handler.Core.HanlderService;
using Handler.Core.Payment;

namespace Handler.Core.Abstractions.Services;

public interface IPaymentService
{
    int CalculatePayment(Product[] products, List<BucketItem> productIdsAndQuantity);
    List<PaymentType> GetPaymentTypes();

    (string? error, string? cheque) ConfirmPayment(TemporyOrder order, Payment paymentInfo);
}

