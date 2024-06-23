using OrderService.Domain.Models;

namespace OrderService.Api.Contracts.Store;

public record StoreGetLast(
    Guid Id,
    List<BasketItem> Basket,
    string Comment,
    TimeSpan CookingTime,
    DateTime OrderDate,
    DateTime? CookingDate);