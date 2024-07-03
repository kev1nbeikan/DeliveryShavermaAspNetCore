using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Common;
using OrderService.Domain.Common.Code;

namespace OrderService.Api.Contracts.Client;

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