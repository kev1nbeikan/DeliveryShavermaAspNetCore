using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Common;

namespace OrderService.Api.Contracts.Store;

public record StoreGetLast(
    Guid Id,
    List<BasketItem> Basket,
    string Comment,
    TimeSpan CookingTime,
    DateTime OrderDate,
    DateTime? CookingDate);