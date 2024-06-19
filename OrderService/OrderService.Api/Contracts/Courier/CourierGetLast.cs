using Newtonsoft.Json.Linq;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Contracts.Courier;

public record CourierGetLast(Guid Id, Guid StoreId, JObject Basket,
    string Comment, TimeSpan DeliveryTime, DateTime OrderDate,
    DateTime CookingDate, DateTime DeliveryDate);