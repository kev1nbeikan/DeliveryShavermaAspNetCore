using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Services;
using Handler.Core.Common;
using Handler.Core.HanlderService;
using Handler.Core.Payment;
using HandlerService.Infustucture.Extensions;

namespace HandlerService.Application.Services;

public class PaymentService : IPaymentService
{
    public int GetTotalPrice(Product[] products, List<ProductWithAmount> productIdsAndAmount)
    {
        return products.Sum(p =>
            p.Price * productIdsAndAmount.Where(x => p.Id == x.Id).Select(x => x.Quantity).First());
    }

    public List<PaymentType> GetPaymentTypes()
    {
        return [PaymentType.Card, PaymentType.Cash];
    }

    public (string? error, string? cheque) ConfirmPayment(PaymentOrder order,
        Payment paymentInfo)
    {
        return paymentInfo.PaymentType switch
        {
            PaymentType.Cash => (null, "Cheque for Cash: " + order.Price),
            PaymentType.Card => ProccessCardPayment(order, paymentInfo.Card),
            _ => ("payment type not found", null)
        };
    }

    private static (string? error, string? cheque) ProccessCardPayment(PaymentOrder order, Card? paymentInfoCard)
    {
        var error = IsValid(paymentInfoCard);
        if (error.HasValue()) return (error, null);
        return ("", "Cheque for Card: " + order.Price);
    }

    private static string? IsValid(Card? card)
    {
        if (card is null) return "карта не указана";
        if (string.IsNullOrEmpty(card.CardNumber)) return "неверный номер карты";
        if (string.IsNullOrEmpty(card.ExpiryDate)) return "неверная дата окончания карты";
        if (string.IsNullOrEmpty(card.CVV)) return "неверный код CVV";
        return null;
    }
}