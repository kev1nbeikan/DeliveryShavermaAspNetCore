using System.Security.Claims;
using StoreService.Core;
using UserService.Core.Common;

namespace StoreService.Main.Extensions;

public static class UserClaimsExtensions
{
    public static Guid UserId(this ClaimsPrincipal user)
    {
        return Guid.Parse(user.FindFirstValue(UserClaimsStrings.UserId) ?? string.Empty);
    }

    public static RoleCode Role(this ClaimsPrincipal user)
    {
        return RoleCode.Client;
        // return user.FindFirstValue(UserClaims.Role);
    }
}