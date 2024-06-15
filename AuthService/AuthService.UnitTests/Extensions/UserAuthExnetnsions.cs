using AuthService.Core;

namespace AuthService.UnitTests.Extensions;

public static class UserAuthExtensions
{
    public static bool IsEqual(this UserAuth userAuth, UserAuth other)
    {
        return userAuth.Id == other.Id
               && userAuth.Email == other.Email
               && userAuth.PasswordHash == other.PasswordHash;
    }
}