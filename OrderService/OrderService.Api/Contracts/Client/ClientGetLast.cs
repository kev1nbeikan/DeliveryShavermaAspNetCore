using Newtonsoft.Json.Linq;

namespace OrderService.Api.Contracts.Client;

public record ClientGetLast(
    Guid Id,
    string Basket,
    int Price,
    string Comment,
    DateTime OrderDate,
    DateTime? DeliveryDate,
    string Cheque);