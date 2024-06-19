using Newtonsoft.Json.Linq;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Contracts.Store;

public record StoreGetLast(
    Guid Id,
    JObject Basket,
    string Comment,
    TimeSpan CookingTime,
    DateTime OrderDate,
    DateTime CookingDate);