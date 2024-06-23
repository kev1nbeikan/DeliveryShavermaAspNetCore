using System.Security.Claims;

namespace OrderService.Api.Extensions;

public static class UserClaimsExtensions
{
    public static Guid UserId(this ClaimsPrincipal user)
    {
        return Guid.Parse(user.FindFirstValue(UserClaims.UserId) ?? string.Empty);
    }

    public static string Role(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(UserClaims.Role) ?? string.Empty;
    }
}