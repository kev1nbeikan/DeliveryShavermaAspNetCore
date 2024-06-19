using Newtonsoft.Json.Linq;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Contracts.Client;

public record ClientGetCanceled(Guid Id, JObject Basket, int Price
    , string Comment, string ClientAddress, string ClientNumber, 
    DateTime OrderDate, string Cheque, StatusCode LastStatus, string ReasonOfCanceled);