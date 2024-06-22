using Newtonsoft.Json.Linq;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Contracts.Client;

public record ClientGetCanceled(
    Guid Id,
    string Basket,
    int Price,
    string Comment,
    DateTime OrderDate,
    string Cheque,
    StatusCode LastStatus,
    string ReasonOfCanceled);