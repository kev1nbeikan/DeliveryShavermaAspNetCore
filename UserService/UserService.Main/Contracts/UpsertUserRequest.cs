namespace UserService.Main.Contracts;

public record UpsertUserRequest(
    Guid UserId,
    string Address,
    string Comment,
    string PhoneNumber);