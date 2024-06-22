using System.Security.Claims;
using Handler.Core;

namespace HandlerService.Middlewares;

public class UserMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        var userIdString = GetFromCookiesOrHeaders(context, UserClaims.UserId);
        var roleString = GetFromCookiesOrHeaders(context, UserClaims.Role);


        if (!Guid.TryParse(userIdString, out Guid userId) || string.IsNullOrEmpty(roleString))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        context.User = new ClaimsPrincipal(
            new ClaimsIdentity(
                new Claim[] { 
                    new (UserClaims.UserId, userId.ToString()),
                    new (UserClaims.Role, roleString)
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