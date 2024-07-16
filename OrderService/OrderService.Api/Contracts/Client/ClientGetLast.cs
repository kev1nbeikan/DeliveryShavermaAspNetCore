using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Common;

namespace OrderService.Api.Contracts.Client;

/// <summary>
///  Dto, истории заказа клиента.
/// </summary>
/// <param name="Id"> Id заказа.</param>
/// <param name="Basket"> Список позиций в заказе.</param>
/// <param name="Price"> Стоимость заказа</param>
/// <param name="Comment"> Комментарий к заказу.</param>
/// <param name="OrderDate"> Дата создания заказа.</param>
/// <param name="DeliveryDate"> Дата доставки.</param>
/// <param name="Cheque"> Чек.</param>
public record ClientGetLast(
    Guid Id,
    List<BasketItem> Basket,
    int Price,
    string Comment,
    DateTime OrderDate,
    DateTime? DeliveryDate,
    string Cheque);