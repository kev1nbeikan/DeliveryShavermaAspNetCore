namespace Handler.Core.Abstractions.Services;

public interface IPaymentService
{
    int CalculatePayment(Product[] products);
    List<PaymentType> GetPaymentTypes();

    (string? error, string? cheque) ConfirmPayment(HandlerServiceOrder order, Payment paymentRequest);
}

