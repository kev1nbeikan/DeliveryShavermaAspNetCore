using System.Net;
using BarsGroupProjectN1.Core.AppSettings;
using Handler.Core.Common;

namespace HandlerService.Extensions;

public static class AppHttpClientsExtensions
{
    public static void AddServicesHttpClients(this IServiceCollection services, ServicesOptions? options)
    {
        ArgumentNullException.ThrowIfNull(options);

        services.AddHttpClient(nameof(options.UsersUrl),
            httpClient => { SetBaseAddressWithLogging(options.UsersUrl, httpClient, nameof(options.UsersUrl)); });

        services.AddHttpClient(nameof(options.MenuUrl),
            httpClient => { SetBaseAddressWithLogging(options.MenuUrl, httpClient, nameof(options.MenuUrl)); });


        services.AddHttpClient(nameof(options.StoreUrl),
            httpClient => { SetBaseAddressWithLogging(options.StoreUrl, httpClient, nameof(options.StoreUrl)); });
    }

    private static void SetBaseAddressWithLogging(string url, HttpClient httpClient, string httpClientName)
    {
        httpClient.BaseAddress =
            new Uri(url ?? throw new Exception($"{httpClientName} not found"));
        Console.WriteLine($"{httpClientName}: {httpClient.BaseAddress}");
    }
}