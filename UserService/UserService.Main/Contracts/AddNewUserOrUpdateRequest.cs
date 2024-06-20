namespace UserService.Main.Contracts;

public record AddNewUserOrUpdateRequest(
    Guid UserId,
    string Comment,
    string Address,
    string PhoneNumber,
    String StoreId);