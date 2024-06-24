using System.Security.Claims;
using CourierService.Core.Common;
using CourierService.Core.Models.Code;

namespace CourierService.API.Extensions;

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