
using Handler.Core;
using Handler.Core.Abstractions;

namespace HandlerService.Application.Services;

public class PaymentService : IPaymentService
{
    public long CalculatePayment(Product[] products)
    {
        return products.Sum(x => x.Price);
    }

    public List<PaymentType> GetPaymentTypes()
    {
        return [PaymentType.Card, PaymentType.Cash];
    }
}