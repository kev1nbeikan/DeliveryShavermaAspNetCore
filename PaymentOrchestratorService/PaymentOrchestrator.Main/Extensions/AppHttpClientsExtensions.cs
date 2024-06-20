using Handler.Core.Common;
using Microsoft.Extensions.Options;

namespace HandlerService.Extensions;

public static class AppHttpClientsExtensions
{
    public static void AddServicesHttpClient(this IServiceCollection services, ServicesOptions? options)
    {
        ArgumentNullException.ThrowIfNull(options);

        services.AddHttpClient(nameof(options.UsersUrl),
            httpClient =>
            {
                httpClient.BaseAddress =
                    new Uri(options.UsersUrl ?? throw new Exception("paymentUrl not found"));
            });
    }
}