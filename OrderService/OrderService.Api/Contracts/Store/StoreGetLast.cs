using Newtonsoft.Json.Linq;

namespace OrderService.Api.Contracts.Store;

public record StoreGetLast(
    Guid Id,
    string Basket,
    string Comment,
    TimeSpan CookingTime,
    DateTime OrderDate,
    DateTime? CookingDate);