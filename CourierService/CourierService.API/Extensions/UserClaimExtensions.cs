using System.Security.Claims;
using BarsGroupProjectN1.Core.Models.Courier;
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
		return RoleCode.Courier;
		// return user.FindFirstValue(UserClaims.Role);
	}

	public static CourierStatusCode Code(this ClaimsPrincipal user)
	{
		return (CourierStatusCode) Enum.Parse(
			typeof(CourierStatusCode),
			user.FindFirstValue(UserClaimsStrings.Code) ?? CourierStatusCode.NotWork.ToString()
		);
	}
}