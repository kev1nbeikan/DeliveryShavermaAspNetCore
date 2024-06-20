using Microsoft.Extensions.Options;
using UserService.Core.Common;

namespace UserService.Main.Utils;

public static class AppBuilderUtils
{
    public static string GetOriginsString(IOptions<ServiceOptions> options)
    {
        return string.Join("; ", [
            options.Value.MenuUrl,
            options.Value.UsersUrl,
            options.Value.PaymentOrchestratorUrl,
        ]);
    }
}