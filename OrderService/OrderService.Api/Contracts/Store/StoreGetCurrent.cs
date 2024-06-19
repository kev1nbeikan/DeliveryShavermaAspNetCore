using Newtonsoft.Json.Linq;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Contracts.Store;

public record StoreGetCurrent(
    Guid Id,
    StatusCode Status,
    JObject Basket,
    string Comment,
    string CourierNumber,
    TimeSpan CookingTime);