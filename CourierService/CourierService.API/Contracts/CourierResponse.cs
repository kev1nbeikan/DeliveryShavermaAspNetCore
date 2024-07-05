using BarsGroupProjectN1.Core.Models.Courier;
using CourierService.Core.Models.Code;

namespace CourierService.API.Contracts;

public record CourierResponse(Guid Id, CourierStatusCode Status);