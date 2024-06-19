using Handler.Core.Common;
using Microsoft.Extensions.Options;

namespace HandlerService.Utils;

public static class AppBuilderUtils
{
    public static string GetOriginsString(IOptions<ServicesOptions> options)
    {
        var origins = string.Join(" ", [
            options.Value.MenuUrl,
            options.Value.UsersUrl,
        ]);
        Console.WriteLine(origins);
        return origins;
    }
}