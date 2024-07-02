using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models;

namespace BarsGroupProjectN1.Core.Repositories;

public abstract class RepositoryHttpClientBase
{
    protected readonly HttpClient _httpClient;

    protected RepositoryHttpClientBase(string httpClientName, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(httpClientName);
        _httpClient.DefaultRequestHeaders.Add(UserClaimsStrings.Role, ((int)RoleCode.Admin).ToString());
    }
}