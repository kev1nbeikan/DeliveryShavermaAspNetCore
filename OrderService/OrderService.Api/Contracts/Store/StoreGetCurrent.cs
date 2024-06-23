using OrderService.Domain.Models;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Contracts.Store;

public record StoreGetCurrent(
    Guid Id,
    StatusCode Status,
    List<BasketItem> Basket,
    string Comment,
    string CourierNumber,
    TimeSpan CookingTime);