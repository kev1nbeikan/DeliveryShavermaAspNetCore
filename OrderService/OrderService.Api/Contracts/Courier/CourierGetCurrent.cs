using BarsGroupProjectN1.Core.Models.Order;

namespace OrderService.Api.Contracts.Courier;

/// <summary>
///  Dto, текущего заказа курьера.
/// </summary>
/// <param name="Id"> Id заказа.</param>
/// <param name="ClientId"> Id клиента.</param>
/// <param name="Status"> Статус.</param>
/// <param name="Basket"> Корзина.</param>
/// <param name="Comment"> Комментарий.</param>
/// <param name="StoreAddress"> Адрес магазина.</param>
/// <param name="ClientAddress"> Адрес клиента.</param>
/// <param name="ClientNumber"> Номер клиента.</param>
/// <param name="DeliveryTime"> Время доставки.</param>
/// <param name="CookingTime"> Время приготовления.</param>
public record CourierGetCurrent(
    Guid Id,
    Guid ClientId,
    StatusCode Status,
    List<BasketItem> Basket,
    string Comment,
    string StoreAddress,
    string ClientAddress,
    string ClientNumber,
    TimeSpan DeliveryTime,
    TimeSpan CookingTime);