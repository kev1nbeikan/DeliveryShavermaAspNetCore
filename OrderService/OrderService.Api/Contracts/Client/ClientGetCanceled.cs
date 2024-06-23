using OrderService.Domain.Models;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Contracts.Client;

public record ClientGetCanceled(
    Guid Id,
    List<BasketItem> Basket,
    int Price,
    string Comment,
    DateTime OrderDate,
    string Cheque,
    StatusCode LastStatus,
    string ReasonOfCanceled);