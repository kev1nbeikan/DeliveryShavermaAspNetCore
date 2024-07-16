
using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Common;

namespace OrderService.Api.Contracts.Courier;

/// <summary>
///  Dto, истории заказа курьера.
/// </summary>
/// <param name="Id"> Id заказа.</param>
/// <param name="StoreId"> Id магазина.</param>
/// <param name="Basket"> Список позиций в заказе.</param>
/// <param name="Comment"> Комментарий к заказу.</param>
/// <param name="DeliveryTime"> Время доставки.</param>
/// <param name="OrderDate"> Дата создания заказа.</param>
/// <param name="CookingDate"> Дата приготовления</param>
/// <param name="DeliveryDate"> Дата доставки</param>
public record CourierGetLast(
    Guid Id,
    Guid StoreId,
    List<BasketItem> Basket,
    string Comment,
    TimeSpan DeliveryTime,
    DateTime OrderDate,
    DateTime? CookingDate,
    DateTime? DeliveryDate);