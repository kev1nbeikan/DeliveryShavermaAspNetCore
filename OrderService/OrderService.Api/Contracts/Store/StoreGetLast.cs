using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Common;

namespace OrderService.Api.Contracts.Store;

/// <summary>
///  Dto, истории заказа магазина.
/// </summary>
/// <param name="Id"> Id заказа.</param>
/// <param name="Basket"> Список позиций в заказе.</param>
/// <param name="Comment"> Комментарий к заказу.</param>
/// <param name="CookingTime"> Время приготовления.</param>
/// <param name="OrderDate"> Дата создания заказа.</param>
/// <param name="CookingDate"> Дата приготовления</param>
public record StoreGetLast(
    Guid Id,
    List<BasketItem> Basket,
    string Comment,
    TimeSpan CookingTime,
    DateTime OrderDate,
    DateTime? CookingDate);