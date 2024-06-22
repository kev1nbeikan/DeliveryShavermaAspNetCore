using System.Net;
using Handler.Core.Common;

namespace HandlerService.Extensions;

public static class AppHttpClientsExtensions
{
    public static void AddServicesHttpClients(this IServiceCollection services, ServicesOptions? options)
    {
        ArgumentNullException.ThrowIfNull(options);

        services.AddHttpClient(nameof(options.UsersUrl),
            httpClient =>
            {
                httpClient.BaseAddress =
                    new Uri(options.UsersUrl ?? throw new Exception($"{nameof(options.UsersUrl)} not found"));
                Console.WriteLine($"user url:{httpClient.BaseAddress}");

            });

        services.AddHttpClient(nameof(options.MenuUrl),
            httpClient =>
            {
                httpClient.BaseAddress =
                    new Uri(options.MenuUrl ?? throw new Exception($"{nameof(options.MenuUrl)} not found"));
                Console.WriteLine($"menu url:{httpClient.BaseAddress}");
            });
    }
}