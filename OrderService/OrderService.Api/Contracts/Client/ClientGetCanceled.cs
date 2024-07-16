using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;

namespace OrderService.Api.Contracts.Client;

/// <summary>
///  Dto, отмененного заказа клиента.
/// </summary>
/// <param name="Id"> Id заказа.</param>
/// <param name="Basket"> Корзина.</param>
/// <param name="Price"> Цена.</param>
/// <param name="Comment"> Комментарии.</param>
/// <param name="OrderDate"> Дата создания.</param>
/// <param name="CanceledDate"> Дата отмены.</param>
/// <param name="Cheque"> Чек.</param>
/// <param name="LastStatus"> Последний статус.</param>
/// <param name="ReasonOfCanceled"> Причина отмены.</param>
/// <param name="WhoCanceled"> Кто отменил.</param>
public record ClientGetCanceled(
    Guid Id,
    List<BasketItem> Basket,
    int Price,
    string Comment,
    DateTime OrderDate,
    DateTime CanceledDate,
    string Cheque,
    StatusCode LastStatus,
    string ReasonOfCanceled,
    RoleCode WhoCanceled);