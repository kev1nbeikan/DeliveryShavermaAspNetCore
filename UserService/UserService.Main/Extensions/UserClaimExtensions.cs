using System.Security.Claims;
using UserService.Core.Common;

namespace UserService.Main.Extensions;

public static class UserClaimsExtensions
{
    public static Guid UserId(this ClaimsPrincipal user)
    {
        return Guid.Parse(user.FindFirstValue(UserClaimsStrings.UserId) ?? string.Empty);
        return Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa3");
    }

    public static RoleCode Role(this ClaimsPrincipal user)
    {
        return RoleCode.Client;
        // return user.FindFirstValue(UserClaims.Role);
    }
}