using Microsoft.Extensions.Options;
using OrderService.Domain.Common;

namespace OrderService.Api.Extensions;

/// <summary>
/// Расширение для добавления политики CORS
/// </summary>
public static class ApiCorsExtensions
{
    /// <summary>
    /// Добавить политику CORS
    /// </summary>
    /// <param name="services"> IApplicationBuilder типа this</param>
    /// <param name="optionsServices"> IOptions [ServicesOptions] ? </param>
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