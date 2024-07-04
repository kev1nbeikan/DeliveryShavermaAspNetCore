using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Common;
using OrderService.Domain.Common.Code;

namespace OrderService.Api.Contracts.Courier;

public record CourierGetCurrent(
    Guid Id,
    Guid ClientId,
    StatusCode Status,
    List<BasketItem> Basket,
    string Comment,
    string StoreAddress,
    string ClientAddress,
    string ClientNumber,
    TimeSpan DeliveryTime,
    TimeSpan CookingTime);