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
        if (string.IsNullOrEmpty(context.Request.Headers["userId"]))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        if (!Guid.TryParse(context.Request.Headers["userId"], out Guid userId))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        context.User = new ClaimsPrincipal(new[]
            { new ClaimsIdentity(new Claim[] { new Claim("userId", userId.ToString()) }) });
        
        await _next(context);
    }
}