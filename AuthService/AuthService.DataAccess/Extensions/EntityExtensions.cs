using AuthService.Core;
using AuthService.DataAccess.Entities;

namespace AuthService.DataAccess.Extensions;

public static class EntityExtensions
{
    public static CourierAuth? ToCore(this CurierAuthEntity curier)
    {
        return new CourierAuth
        {
            Id = curier.Id,
            Login = curier.Login,
            PasswordHash = curier.PasswordHash
        };
    }

    public static StoreAuth ToCore(this StoreAuthEntity store)
    {
        return new StoreAuth
        {
            Id = store.Id,
            Login = store.Login,
            PasswordHash = store.PasswordHash
        };
    }

    public static UserAuth ToCore(this UserAuthEntity user)
    {
        return new UserAuth
        {
            Id = user.Id,
            Email = user.Email,
            PasswordHash = user.PasswordHash
        };
    }
}