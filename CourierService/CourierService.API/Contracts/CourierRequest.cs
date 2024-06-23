namespace CourierService.API.Contracts;

public record CourierRequest(string email, string password, bool status);