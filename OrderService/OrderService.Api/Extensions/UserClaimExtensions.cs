using System.Security.Claims;

namespace OrderService.Api.Extensions;

public static class UserClaimsExtensions
{
    public static Guid IdOfRequest = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa3");
    public static string RoleOfRequest = "0";
    
    
    public static Guid UserId(this ClaimsPrincipal user)
    {
        return IdOfRequest;
    }

    public static string Role(this ClaimsPrincipal user)
    {
        return RoleOfRequest;
    }
}