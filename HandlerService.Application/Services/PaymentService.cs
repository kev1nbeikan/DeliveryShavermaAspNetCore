using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Services;

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

    public (string? error, string? cheque) ConfirmPayment(HandlerServiceOrder order,
        Payment paymentRequest)
    {
        return ("", "");
    }
}