using Newtonsoft.Json.Linq;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Contracts.Courier;

public record CourierGetLast(
    Guid Id,
    JObject Basket,
    string Comment,
    string StoreAddress,
    TimeSpan DeliveryTime,
    DateTime OrderDate,
    DateTime CookingDate,
    DateTime DeliveryDate);