using Handler.Core.Common;
using Handler.Core.HanlderService;
using Handler.Core.Payment;

namespace Handler.Core.Abstractions.Services;

public interface IPaymentService
{
    int GetTotalPrice(Product[] products, List<ProductWithAmount> productIdsAndAmount);
    
    /// <summary>
    /// Возвращает все доступные типы оплат
    /// </summary>
    List<PaymentType> GetPaymentTypes();

    
    /// <summary>
    /// Подтверждение оплаты в зависимости от типа оплаты и возвращение чека.
    /// </summary>
    /// <param name="order">Заявка на оплату.</param>
    /// <param name="paymentInfo">Информация о платеже.</param>
    /// <returns>Кортеж, содержащий сообщение об ошибке (если есть) и чек.</returns>
    (string? error, string? cheque) ConfirmPayment(PaymentOrder order, Payment paymentInfo);
}

