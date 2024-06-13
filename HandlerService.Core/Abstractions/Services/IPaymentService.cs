namespace Handler.Core.Abstractions;

public interface IPaymentService
{
    int CalculatePayment(Product[] products);
    List<PaymentType> GetPaymentTypes();

    (string? error, string? check) ConfirmPayment(HandlerServiceOrder order, string paymentType, string cardNumber,
        string expiryDate,
        string cvv,
        string comment, string address);
}