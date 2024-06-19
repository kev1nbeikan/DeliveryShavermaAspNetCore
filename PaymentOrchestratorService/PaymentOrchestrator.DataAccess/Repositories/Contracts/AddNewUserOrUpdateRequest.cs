using Handler.Core.Payment;

namespace HandlerService.DataAccess.Repositories.Contracts;

public record AddNewUserOrUpdateRequest(
    Guid UserId,
    List<BucketItem> ProductIdsAndQuantity,
    string Comment,
    string Address,
    string PhoneNumber,
    String StoreId);