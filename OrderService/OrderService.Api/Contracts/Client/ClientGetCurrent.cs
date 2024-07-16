using BarsGroupProjectN1.Core.Models.Order;

namespace OrderService.Api.Contracts.Client;

/// <summary>
///  Dto, текущего заказа клиента
/// </summary>
/// <param name="Id"> Id заказа.</param>
/// <param name="ClientId"> Id клиента.</param>
/// <param name="StoreId"> Id магазина.</param>
/// <param name="CourierId"> Id курьера.</param>
/// <param name="Status"> Статус.</param>
/// <param name="Basket"> Корзина.</param>
/// <param name="Price"> Цена.</param>
/// <param name="Comment"> Комментарии.</param>
/// <param name="ClientAddress"> Адрес доставки.</param>
/// <param name="CourierNumber"> Номер курьера.</param>
/// <param name="ClientNumber"> Номер клиента.</param>
/// <param name="Cheque"> Чек.</param>
public record ClientGetCurrent(
    Guid Id,
    Guid ClientId,
    Guid StoreId,
    Guid CourierId,
    StatusCode Status,
    List<BasketItem> Basket,
    int Price,
    string Comment,
    string ClientAddress,
    string CourierNumber,
    string ClientNumber,
    string Cheque);