using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Common;

namespace OrderService.Api.Contracts.Order;

/// <summary>
/// Dto, представляющее запрос на создание заказа.
/// </summary>
/// <param name="Id"> Id заказа.</param>
/// <param name="ClientId"> Id клиента.</param>
/// <param name="CourierId"> Id курьера.</param>
/// <param name="StoreId"> Id магазина.</param>
/// <param name="Basket"> Корзина.</param>
/// <param name="Price"> Цена.</param>
/// <param name="Comment"> Комментарий.</param>
/// <param name="StoreAddress"> Адрес магазина.</param>
/// <param name="ClientAddress"> Адрес клиента.</param>
/// <param name="CourierNumber"> Номер курьера.</param>
/// <param name="ClientNumber"> Номер клиента.</param>
/// <param name="CookingTime"> Время приготовления.</param>
/// <param name="DeliveryTime"> Время доставки.</param>
/// <param name="Cheque"> Чек.</param>
public record OrderCreateRequest(
    Guid Id,
    Guid ClientId,
    Guid CourierId,
    Guid StoreId,
    List<BasketItem> Basket,
    int Price,
    string Comment,
    string StoreAddress,
    string ClientAddress,
    string CourierNumber,
    string ClientNumber,
    TimeSpan CookingTime,
    TimeSpan DeliveryTime,
    string Cheque);