using Handler.Core;
using Handler.Core.Abstractions;

namespace HandlerService.Application.Services;

public class PaymentService : IPaymentService
{
    public int CalculatePayment(Product[] products)
    {
        return (int)products.Sum(x => x.Price);
    }

    public List<PaymentType> GetPaymentTypes()
    {
        return [PaymentType.Card, PaymentType.Cash];
    }

    public (string? error, string? check) ConfirmPayment(HandlerServiceOrder order, string paymentType,
        string cardNumber,
        string expiryDate,
        string cvv, string comment, string address)
    {
        return ("", "");
    }
}