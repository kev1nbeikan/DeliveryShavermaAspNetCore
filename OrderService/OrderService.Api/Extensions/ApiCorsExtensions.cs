using Microsoft.Extensions.Options;
using OrderService.Domain.Common;

namespace OrderService.Api.Extensions;

public static class ApiCorsExtensions
{
    public static void SetCorsPolicies(this IApplicationBuilder services, IOptions<ServicesOptions>? optionsServices)
    {
        ArgumentNullException.ThrowIfNull(optionsServices);
        services.UseCors(
            policyBuilder => policyBuilder
                .AllowAnyHeader()
                .WithOrigins(optionsServices.Value.MenuUrl, optionsServices.Value.UsersUrl,
                    optionsServices.Value.PaymentOrchestratorUrl, optionsServices.Value.StoreUrl,
                    optionsServices.Value.CouriersUrl)
                .Build()
        );
    }
}