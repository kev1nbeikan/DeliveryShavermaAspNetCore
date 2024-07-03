using BarsGroupProjectN1.Core.AppSettings;
using Microsoft.Extensions.Options;

namespace AuthService.Main.Extensions;

public static class ApiCorsExtensions
{
    public static void SetCorsPolicies(this IApplicationBuilder services, IOptions<ServicesOptions> optionsServices)
    {
        services.UseCors(
            policyBuilder => policyBuilder.WithOrigins(
                    optionsServices.Value.MenuUrl,
                    optionsServices.Value.UsersUrl,
                    optionsServices.Value.PaymentOrchestratorUrl,
                    optionsServices.Value.AuthUrl,
                    optionsServices.Value.ChatUrl
                )
                .AllowAnyHeader().AllowAnyMethod().AllowCredentials()
                
                .Build()
        );
    }
}