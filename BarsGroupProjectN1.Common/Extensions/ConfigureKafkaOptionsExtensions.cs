using BarsGroupProjectN1.Core.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarsGroupProjectN1.Core.Extensions;

public static class ConfigureKafkaOptionsExtensions
{
    public static void ConfigureKafkaOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaOptions>(configuration.GetSection("KafkaOptions"));
    }
}