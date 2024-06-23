using Newtonsoft.Json.Linq;

namespace OrderService.Api.Contracts.Courier;

public record CourierGetLast(
    Guid Id,
    Guid StoreId,
    string Basket,
    string Comment,
    TimeSpan DeliveryTime,
    DateTime OrderDate,
    DateTime? CookingDate,
    DateTime? DeliveryDate);