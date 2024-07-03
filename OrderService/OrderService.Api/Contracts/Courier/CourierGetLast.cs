
using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Common;

namespace OrderService.Api.Contracts.Courier;

public record CourierGetLast(
    Guid Id,
    Guid StoreId,
    List<BasketItem> Basket,
    string Comment,
    TimeSpan DeliveryTime,
    DateTime OrderDate,
    DateTime? CookingDate,
    DateTime? DeliveryDate);