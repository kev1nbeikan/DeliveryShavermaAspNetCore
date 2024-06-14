using HandlerService.Controllers;

namespace HandlerService.DataAccess.Contracts;

public record CurierWithDeliveryTimeResponse(
    Curier? Curier,
    TimeSpan DeliveryTime
);