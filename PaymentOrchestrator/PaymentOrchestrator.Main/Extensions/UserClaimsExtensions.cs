using System.Security.Claims;
using Handler.Core;

namespace HandlerService.Extensions;

public static class UserClaimsExtensions
{
    public static Guid UserId(this ClaimsPrincipal user)
    {
        // return Guid.Parse(user.FindFirstValue(UserClaims.UserId) ?? string.Empty);
        return Guid.NewGuid();
    }
}