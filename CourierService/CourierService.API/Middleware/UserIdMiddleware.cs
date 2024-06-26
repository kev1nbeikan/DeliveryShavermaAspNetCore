using System.Security.Claims;
using CourierService.Core.Common;

namespace CourierService.API.Middleware;

public class UserIdMiddleware(RequestDelegate next)
{
	private readonly RequestDelegate _next = next;

	public async Task Invoke(HttpContext context)
	{
		var userIdString = GetFromCookiesOrHeaders(context, UserClaimsStrings.UserId);
		var roleString = GetFromCookiesOrHeaders(context, UserClaimsStrings.Role);
		var codeString = GetFromCookiesOrHeaders(context, UserClaimsStrings.Code);

		if (!Guid.TryParse(userIdString, out Guid userId) || string.IsNullOrEmpty(roleString) ||
		    string.IsNullOrEmpty(codeString))
		{
			await _next(context);
			return;
		}

		context.User = new ClaimsPrincipal(
			new ClaimsIdentity(
				new Claim[]
				{
					new(UserClaimsStrings.UserId, userId.ToString()),
					new(UserClaimsStrings.Role, roleString),
					new(UserClaimsStrings.Code, codeString)
				}
			)
		);

		await _next(context);
	}

	private string GetFromCookiesOrHeaders(HttpContext context, string key)
	{
		return context.Request.Cookies[key] ??
		       context.Request.Headers[key].ToString();
	}
}