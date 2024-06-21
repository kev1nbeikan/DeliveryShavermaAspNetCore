using Handler.Core.Common;
using HandlerService.DataAccess.Repositories.MessageHandler;

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
            });
    }
}