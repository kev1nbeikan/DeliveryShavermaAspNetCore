using Handler.Core.Common;
using Microsoft.Extensions.Options;

namespace HandlerService.DataAccess.Utils;

public static class HttpClientBuilder
{
    public static HttpClient CreateHttpClient(string baseUrl)
    {
        return new HttpClient()
        {
            BaseAddress = new Uri(baseUrl),
            DefaultRequestHeaders =
            {
            }
        };
    }
}