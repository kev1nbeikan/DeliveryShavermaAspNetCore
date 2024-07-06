using System.Security.AccessControl;
using System.Security.Claims;
using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BarsGroupProjectN1.Core.Middlewares;
/// <summary>
/// Промежуточное ПО для извлечения информации о пользователе из куки или заголовков и перенаправления на страницу ошибки 401 в случае отсутствия информации.
/// </summary>
public class UserIdMiddleware(
    RequestDelegate next,
    ILogger<UserIdMiddleware> Logger)
{
    private readonly RequestDelegate _next = next;
    
    public readonly string UnauthorizedUserViewUrl = "http://localhost:5025/home/unauthorizedUser";


    public async Task Invoke(HttpContext context)
    {
        var userIdString = GetFromCookiesOrHeaders(context, UserClaimsStrings.UserId);
        var roleString = GetFromCookiesOrHeaders(context, UserClaimsStrings.Role);


        if (roleString == ((int)RoleCode.Admin).ToString())
        {
            Logger.LogInformation($"Get admin user {userIdString}");
      
            await _next(context);
            return;
        }

        if (!Guid.TryParse(userIdString, out Guid userId) || string.IsNullOrEmpty(roleString))
        {
            Logger.LogInformation(
                $"Get unauthorized user {userIdString} with role {roleString} redirecting to {UnauthorizedUserViewUrl}");
            
            context.Response.Redirect(UnauthorizedUserViewUrl);
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
        
        Logger.LogInformation($"Get authorized user {userIdString} with role {roleString}");


        await _next(context);
    }

    private string GetFromCookiesOrHeaders(HttpContext context, string key)
    {
        return context.Request.Cookies[key] ??
               context.Request.Headers[key].ToString();
    }
}