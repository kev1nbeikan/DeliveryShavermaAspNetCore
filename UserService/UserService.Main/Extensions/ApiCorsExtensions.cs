using Microsoft.Extensions.Options;
using UserService.Core.Common;

namespace UserService.Main.Extensions;

public static class ApiCorsExtensions
{
    public static void SetCorsPolicies(this IApplicationBuilder services, IOptions<ServiceOptions>? optionsServices)
    {
        ArgumentNullException.ThrowIfNull(optionsServices);

        services.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        // services.UseCors(
        //     policyBuilder => policyBuilder
        //         .AllowAnyHeader()
        //         .WithOrigins(optionsServices.Value.MenuUrl, optionsServices.Value.UsersUrl)
        //         .Build()
        // );
    }
}