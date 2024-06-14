using Handler.Core.HanlderService;

namespace Handler.Core.Abstractions.Services;

public interface IPaymentService
{
    int CalculatePayment(Product[] products);
    List<PaymentType> GetPaymentTypes();

    (string? error, string? cheque) ConfirmPayment(TemporyOrder order, Payment paymentRequest);
}

