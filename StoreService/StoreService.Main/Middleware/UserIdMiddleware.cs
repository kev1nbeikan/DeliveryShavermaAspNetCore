using System.Security.Claims;
using Microsoft.Extensions.Options;
using BarsGroupProjectN1.Core.AppSettings;
using UserClaimsStrings = StoreService.Core.UserClaimsStrings;

namespace StoreService.Main.Middleware;

public class UserIdMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context, IOptions<ServicesOptions> options)
    {
        var userIdString = GetFromCookiesOrHeaders(context, UserClaimsStrings.UserId);
        var roleString = GetFromCookiesOrHeaders(context, UserClaimsStrings.Role);


        if (!Guid.TryParse(userIdString, out Guid userId) || string.IsNullOrEmpty(roleString))
        {
            context.Response.Redirect(options.Value.AuthUrl);
            return;
        }

        context.User = new ClaimsPrincipal(
            new ClaimsIdentity(
                new Claim[]
                {
                    new(UserClaimsStrings.UserId, userId.ToString()),
                    new(UserClaimsStrings.Role, roleString)
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