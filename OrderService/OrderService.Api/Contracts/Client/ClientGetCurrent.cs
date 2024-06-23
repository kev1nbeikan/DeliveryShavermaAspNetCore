using OrderService.Domain.Models;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Contracts.Client;

public record ClientGetCurrent(
    Guid Id,
    StatusCode Status,
    List<BasketItem> Basket,
    int Price,
    string Comment,
    string ClientAddress,
    string CourierNumber,
    string ClientNumber,
    string Cheque);