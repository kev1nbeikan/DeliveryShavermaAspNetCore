namespace Handler.Core.Abstractions;

public interface IPaymentService
{
    long CalculatePayment(Product[] products);
    List<PaymentType> GetPaymentTypes();

    (string? error, string? check) ConfirmPayment(Order order, string paymentType, string cardNumber, string expiryDate,
        string cvv,
        string comment, string address);
}