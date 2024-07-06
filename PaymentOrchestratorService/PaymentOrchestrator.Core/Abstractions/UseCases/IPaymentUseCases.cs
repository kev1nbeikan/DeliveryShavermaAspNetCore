using BarsGroupProjectN1.Core.Contracts.Orders;
using Handler.Core.Common;
using Handler.Core.HanlderService;
using Handler.Core.Payment;

namespace Handler.Core.Abstractions.UseCases;

public interface IPaymentUseCases
{
    
    /// <summary>
    /// Выполняет создание платежа.
    /// </summary>
    /// <param name="productIdsAndQuantity">Список идентификаторов продуктов и их количества.</param>
    /// <param name="comment">Комментарий к платежу.</param>
    /// <param name="address">Адрес доставки.</param>
    /// <param name="phoneNumber">Номер телефона.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Кортеж из массива продуктов, цены и информации о платеже.</returns>
    public Task<(Product[] products, int price, PaymentOrder? paymentOrder)> ExecutePaymentBuild(
        List<ProductWithAmount> productIdsAndQuantity,
        string comment,
        string address,
        string phoneNumber,
        Guid userId);

    
    /// <summary>
    /// Выполняет подтверждение возможности приготовления заказа и оплату.
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="payment">Информация о платеже.</param>
    /// <returns>Запрос на создание заказа.</returns>
    public Task<OrderCreateRequest> ExecutePaymentConfirm(Guid orderId, Guid userId, Services.Payment payment);
}