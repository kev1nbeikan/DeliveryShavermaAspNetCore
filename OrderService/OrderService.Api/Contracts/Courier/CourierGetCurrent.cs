using Newtonsoft.Json.Linq;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Contracts.Courier;

public record CourierGetCurrent(
    Guid Id,
    StatusCode Status,
    string Basket,
    string Comment,
    string StoreAddress,
    string ClientAddress,
    string ClientNumber,
    TimeSpan DeliveryTime);