using Newtonsoft.Json.Linq;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Contracts.Client;

public record ClientGetLast(
    Guid Id,
    JObject Basket,
    int Price,
    string Comment,
    DateTime OrderDate,
    DateTime DeliveryDate,
    string Cheque);