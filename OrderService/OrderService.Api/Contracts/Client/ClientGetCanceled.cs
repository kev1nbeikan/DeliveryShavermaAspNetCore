using OrderService.Domain.Common;
using OrderService.Domain.Common.Code;

namespace OrderService.Api.Contracts.Client;

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