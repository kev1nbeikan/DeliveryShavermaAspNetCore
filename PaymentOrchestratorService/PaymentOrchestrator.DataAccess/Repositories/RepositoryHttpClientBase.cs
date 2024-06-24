using Handler.Core.Common;
using Microsoft.Extensions.Options;

namespace HandlerService.DataAccess.Repositories;

public abstract class RepositoryHttpClientBase()
{
    protected readonly HttpClient _httpClient;

    public RepositoryHttpClientBase(string httpClientName, IHttpClientFactory httpClientFactory) : this()
    {
        _httpClient = httpClientFactory.CreateClient(httpClientName);
    }
}