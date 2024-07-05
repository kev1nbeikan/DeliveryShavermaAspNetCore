using BarsGroupProjectN1.Core.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarsGroupProjectN1.Core.Extensions;

public static class AppHttpClientsExtensions
{
    public static void AddServicesHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(nameof(ServicesOptions)).Get<ServicesOptions>();

        Console.WriteLine(options.UsersUrl);
        Console.WriteLine(options.MenuUrl);
        Console.WriteLine(options.StoreUrl);
        Console.WriteLine(options.OrderUrl);
        Console.WriteLine(options.CouriersUrl);

        ArgumentNullException.ThrowIfNull(options, "ServicesOptions");

        services.AddHttpClient(options.OrderUrl,
            httpClient =>
            {
                SetBaseAddressWithLogging(options.OrderUrl, httpClient, options.OrderUrl);
                ;
            });

        services.AddHttpClient(nameof(options.UsersUrl),
            httpClient => { SetBaseAddressWithLogging(options.UsersUrl, httpClient, nameof(options.UsersUrl)); });

        services.AddHttpClient(nameof(options.MenuUrl),
            httpClient => { SetBaseAddressWithLogging(options.MenuUrl, httpClient, nameof(options.MenuUrl)); });

        services.AddHttpClient(nameof(options.StoreUrl),
            httpClient => { SetBaseAddressWithLogging(options.StoreUrl, httpClient, nameof(options.StoreUrl)); });

        services.AddHttpClient(nameof(options.CouriersUrl),
            httpClient => { SetBaseAddressWithLogging(options.CouriersUrl, httpClient, nameof(options.CouriersUrl)); });
    }

    private static void SetBaseAddressWithLogging(string url, HttpClient httpClient, string httpClientName)
    {
        httpClient.BaseAddress =
            new Uri(url ?? throw new Exception($"{httpClientName} not found"));
        Console.WriteLine($"{httpClientName}: {httpClient.BaseAddress}");
    }
}