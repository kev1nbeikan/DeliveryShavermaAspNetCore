using System.Security.Claims;
using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models;
using Microsoft.AspNetCore.Http;

namespace BarsGroupProjectN1.Core.Middlewares;

public class UserIdMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        var userIdString = GetFromCookiesOrHeaders(context, UserClaimsStrings.UserId);
        var roleString = GetFromCookiesOrHeaders(context, UserClaimsStrings.Role);


        if (roleString == ((int)RoleCode.Admin).ToString())
        {
            Console.WriteLine("User is admin");
            await _next(context);
            return;
        }

        if (!Guid.TryParse(userIdString, out Guid userId) || string.IsNullOrEmpty(roleString))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
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