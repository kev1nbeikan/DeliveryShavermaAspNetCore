using AuthService.Core;
using BarsGroupProjectN1.Core.AppSettings;
using Microsoft.Extensions.Options;

namespace AuthService.Main.Infostructure;

public static class CookiesSaverBuilder
{
    public static CookiesSaver ForUserAuth(IResponseCookies cookies, IOptions<ServicesOptions> serviceOptions)
    {
        var cookiesSaver = new CookiesSaver(cookies);

        cookiesSaver.SetDomains([
            serviceOptions.Value.UsersUrl,
            serviceOptions.Value.PaymentOrchestratorUrl,
            serviceOptions.Value.OrderUrl,
            serviceOptions.Value.CouriersUrl,
            serviceOptions.Value.ChatUrl,
        ]);

        return cookiesSaver;
    }


    public static CookiesSaver ForStoreAuth(IResponseCookies cookies, IOptions<ServicesOptions> serviceOptions)
    {
        var cookiesSaver = new CookiesSaver(cookies);

        cookiesSaver.SetDomains([
            serviceOptions.Value.OrderUrl,
            serviceOptions.Value.StoreUrl,
            serviceOptions.Value.CouriersUrl,
            serviceOptions.Value.MenuUrl,
            serviceOptions.Value.ChatUrl,

        ]);

        return cookiesSaver;
    }

    public static CookiesSaver ForCourierAuth(IResponseCookies cookies, IOptions<ServicesOptions> serviceOptions)
    {
        var cookiesSaver = new CookiesSaver(cookies);

        cookiesSaver.SetDomains([
            serviceOptions.Value.StoreUrl,
            serviceOptions.Value.CouriersUrl,
            serviceOptions.Value.OrderUrl,
            serviceOptions.Value.ChatUrl
        ]);

        return cookiesSaver;
    }
}