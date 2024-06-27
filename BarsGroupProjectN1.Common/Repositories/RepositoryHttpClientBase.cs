namespace BarsGroupProjectN1.Core.Repositories;

public abstract class RepositoryHttpClientBase
{
    protected readonly HttpClient _httpClient;

    protected RepositoryHttpClientBase(string httpClientName, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(httpClientName);
    }
}