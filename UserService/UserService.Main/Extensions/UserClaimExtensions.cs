using System.Security.Claims;
using UserService.Core.Common;

namespace UserService.Main.Extensions;

public static class UserClaimsExtensions
{
    public static Guid UserId(this ClaimsPrincipal user)
    {
        return Guid.NewGuid();
        return Guid.Parse(user.FindFirstValue(UserClaims.UserId) ?? string.Empty);
    }

    public static string Role(this ClaimsPrincipal user)
    {
        return "User";
        return user.FindFirstValue(UserClaims.Role) ?? string.Empty;
    }
}