using Handler.Core.Common;
using Handler.Core.Payment;

namespace Handler.Core.Abstractions;

public interface IUserRepository
{
    Task<MyUser?> Get(Guid userId);
    Task<string?> Save(MyUser user);

    Task<string?> Save(Guid UserId,
        List<BucketItem> ProductIdsAndQuantity,
        string Comment,
        string Address,
        string PhoneNumber,
        String StoreId);
}