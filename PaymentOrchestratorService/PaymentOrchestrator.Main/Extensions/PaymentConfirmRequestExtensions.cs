using Handler.Core;
using Handler.Core.Abstractions.Services;
using HandlerService.Contracts;

namespace HandlerService.Extensions;

public static class PaymentConfirmRequestExtensions
{
    public static Payment ToPaymentInfo(this PaymentConfirmRequest paymentConfirmRequest)
    {
        var result = new Payment
        {
            PaymentType = paymentConfirmRequest.PaymentType.ToPaymentTypeEnum(),
        };

        if (result.PaymentType == PaymentType.Card)
        {
            result.Card = new Card
            {
                CardNumber = paymentConfirmRequest.CardNumber,
                ExpiryDate = paymentConfirmRequest.ExpiryDate,
                CVV = paymentConfirmRequest.CVV
            };
        }

        return result;
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