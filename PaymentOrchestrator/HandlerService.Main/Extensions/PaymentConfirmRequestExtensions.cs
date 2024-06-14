using Handler.Core;
using Handler.Core.Abstractions.Services;
using HandlerService.Contracts;

namespace HandlerService.Extensions;

public static class PaymentConfirmRequestExtensions
{
    public static Payment ToPayment(this PaymentConfirmRequest paymentConfirmRequest)
    {
        return new Payment
        {
            PaymentType = paymentConfirmRequest.PaymentType.ToPaymentTypeEnum(),
            Card = new Card
            {
                PaymentType = paymentConfirmRequest.PaymentType,
                CardNumber = paymentConfirmRequest.CardNumber,
                ExpiryDate = paymentConfirmRequest.ExpiryDate,
                CVV = paymentConfirmRequest.CVV
            }
        };
    }

    private static PaymentType ToPaymentTypeEnum(this string paymentType)
    {
        return paymentType switch
        {
            "Card" => PaymentType.Card,
            "Cash" => PaymentType.Cash,
            _ => PaymentType.None
        };
    }
}