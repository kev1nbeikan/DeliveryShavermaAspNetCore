using Newtonsoft.Json.Linq;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Contracts.Courier;

public record CourierGetCurrent(Guid Id, Guid StoreId, StatusCode Status,
    JObject Basket, string Comment, string ClientAddress,
    string ClientNumber, TimeSpan DeliveryTime);