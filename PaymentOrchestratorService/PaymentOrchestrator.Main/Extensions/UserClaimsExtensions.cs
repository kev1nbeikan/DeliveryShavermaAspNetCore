using System.Security.Claims;
using Handler.Core;

namespace HandlerService.Extensions;

public static class UserClaimsExtensions
{
    public static Guid UserId(this ClaimsPrincipal user)
    {
        return Guid.NewGuid();
        // return Guid.Parse(user.FindFirstValue(UserClaims.UserId) ?? string.Empty);
    }

    public static string Role(this ClaimsPrincipal user)
    {
        return "User";
        return user.FindFirstValue(UserClaims.Role) ?? string.Empty;
    }
}