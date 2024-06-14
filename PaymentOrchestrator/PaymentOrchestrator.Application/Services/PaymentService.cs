using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Services;
using Handler.Core.HanlderService;

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

    public (string? error, string? cheque) ConfirmPayment(TemporyOrder order,
        Payment paymentInfo)
    {
        return paymentInfo.PaymentType switch
        {
            PaymentType.Cash => (null, "Cheque for Cash: " + order.Price),
            PaymentType.Card => proccessCardPayment(order, paymentInfo.Card),
            _ => ("payment type not found", null)
        };
    }

    private static (string? error, string? cheque) proccessCardPayment(TemporyOrder order, Card? paymentInfoCard)
    {
        if (paymentInfoCard == null) return ("card not found", null);
        return ("", "Cheque for Card: " + order.Price);
    }
}