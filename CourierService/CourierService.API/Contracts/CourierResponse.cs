using CourierService.Core.Models.Code;

namespace CourierService.API.Contracts;

public record CourierResponse(Guid id, string email, string password, CourierStatusCode status);