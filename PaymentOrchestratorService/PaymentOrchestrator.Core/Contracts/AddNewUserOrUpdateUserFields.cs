namespace Handler.Core.Contracts;

public record AddNewUserOrUpdateUserFields(
    Guid UserId,
    string Comment,
    string Address,
    string PhoneNumber);