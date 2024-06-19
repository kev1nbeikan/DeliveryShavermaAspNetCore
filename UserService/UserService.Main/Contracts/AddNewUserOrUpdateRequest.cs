namespace UserService.Main.Contracts;

public record AddNewUserOrUpdateRequest(
    List<BucketItem> ProductIdsAndQuantity,
    string Comment,
    string Address,
    string PhoneNumber,
    String StoreId);