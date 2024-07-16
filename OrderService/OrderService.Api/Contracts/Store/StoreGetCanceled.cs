using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;

namespace OrderService.Api.Contracts.Store;

/// <summary>
/// Dto, отмененного заказ магазина.
/// </summary>
/// <param name="Id"> Id заказа.</param>
/// <param name="Basket"> Корзина.</param>
/// <param name="Comment"> Комментарий.</param>
/// <param name="OrderDate"> Дата создания заказа.</param>
/// <param name="CanceledDate"> Дата отмены.</param>
/// <param name="LastStatus"> Последний статус.</param>
/// <param name="ReasonOfCanceled"> Причина отмены.</param>
/// <param name="WhoCanceled"> Кто отменил.</param>
public record StoreGetCanceled(
    Guid Id,
    List<BasketItem> Basket,
    string Comment,
    DateTime OrderDate,
    DateTime CanceledDate,
    StatusCode LastStatus,
    string ReasonOfCanceled,
    RoleCode WhoCanceled);