using OrderService.Domain.Common;
using OrderService.Domain.Common.Code;

namespace OrderService.Api.Contracts.Store;

public record StoreGetCurrent(
    Guid Id,
    Guid ClientId, // удалить потом
    StatusCode Status,
    List<BasketItem> Basket,
    string Comment,
    string CourierNumber,
    TimeSpan CookingTime,
    DateTime OrderDate);