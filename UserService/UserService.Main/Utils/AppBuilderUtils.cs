using Handler.Core.Common;
using Microsoft.Extensions.Options;

namespace UserService.Main.Utils;

public static class AppBuilderUtils
{
    public static string GetOriginsString(IOptions<ServicesOptions> options)
    {
        return string.Join("; ", [
            options.Value.MenuUrl,
            options.Value.UsersUrl,
            options.Value.PaymentOrchestratorUrl,
        ]);
    }
}