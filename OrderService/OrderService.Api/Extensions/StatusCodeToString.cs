using OrderService.Domain.Common.Code;

namespace OrderService.Api.Extensions;

public static class StatusCodeToString
{
    public static readonly Dictionary<StatusCode, string> StatusMapping =
        new()
        {
            { StatusCode.Cooking, "Готовится" },
            { StatusCode.Delivering, "Доставляется" },
            { StatusCode.WaitingClient, "Прибыл к клиенту" },
            { StatusCode.WaitingCourier, "Ожидает курьра" }
        };
}