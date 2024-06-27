using BarsGroupProjectN1.Core.AppSettings;
using Handler.Core.Common;
using Microsoft.Extensions.Options;

namespace HandlerService.Extensions;

public static class ApiCorsExtensions
{
    public static void SetCorsPolicies(this IApplicationBuilder services, IOptions<ServicesOptions> optionsServices)
    {
        services.UseCors(
            policyBuilder => policyBuilder
                .AllowAnyHeader()
                .WithOrigins(optionsServices.Value.MenuUrl, optionsServices.Value.UsersUrl, optionsServices.Value.PaymentOrchestratorUrl)
                .Build()
        );
    }
}