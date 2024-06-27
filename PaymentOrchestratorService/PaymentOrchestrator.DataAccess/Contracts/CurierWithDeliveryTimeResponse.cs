using Handler.Core.Common;

namespace HandlerService.DataAccess.Contracts;

public record CurierWithDeliveryTimeResponse(
    Curier? Curier,
    TimeSpan DeliveryTime
);