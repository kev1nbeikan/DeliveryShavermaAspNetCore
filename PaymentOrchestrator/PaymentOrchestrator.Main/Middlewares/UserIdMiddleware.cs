using System.Security.Claims;

namespace HandlerService.Middlewares;

public class UserIdMiddleware
{
    private readonly RequestDelegate _next;

    public UserIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var userIdString = GetFromCookiesOrHeaders(context, "userId");
        var roleString = GetFromCookiesOrHeaders(context, "role");


        if (!Guid.TryParse(userIdString, out Guid userId) || string.IsNullOrEmpty(roleString))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }


        context.User = new ClaimsPrincipal(new[]
        {
            new ClaimsIdentity(new Claim[]
            {
                new Claim("userId", userId.ToString()),
                new Claim("role", roleString)
            }),
        });

        await _next(context);
    }

    private string? GetFromCookiesOrHeaders(HttpContext context, string key)
    {
        return context.Request.Headers[key].ToString() ??
               context.Request.Cookies[key]?.ToString();
    }
}