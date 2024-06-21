namespace Handler.Core.Contracts;

public record UpsertFields(
    Guid UserId,
    string Comment,
    string Address,
    string PhoneNumber);
