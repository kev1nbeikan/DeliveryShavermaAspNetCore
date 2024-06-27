using BarsGroupProjectN1.Core.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarsGroupProjectN1.Core.Extensions;

public static class ConfigureServicesOptionsExtensions
{
    public static void ConfigureServiceOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServicesOptions>(configuration.GetSection(nameof(ServicesOptions)));
    }
}