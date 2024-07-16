using BarsGroupProjectN1.Core.Models.Order;

namespace OrderService.Api.Contracts.Store;

/// <summary>
///  Dto, текущего заказа магазина
/// </summary>
/// <param name="Id"> Id заказа.</param>
/// <param name="ClientId"> Id клиента.</param>
/// <param name="Status"> Статус.</param>
/// <param name="Basket"> Корзина.</param>
/// <param name="Comment"> Комментарий.</param>
/// <param name="CourierNumber"> Номер курьера.</param>
/// <param name="CookingTime"> Время готовки.</param>
/// <param name="OrderDate"> Дата создания.</param>
public record StoreGetCurrent(
    Guid Id,
    Guid ClientId, // удалить потом
    StatusCode Status,
    List<BasketItem> Basket,
    string Comment,
    string CourierNumber,
    TimeSpan CookingTime,
    DateTime OrderDate);