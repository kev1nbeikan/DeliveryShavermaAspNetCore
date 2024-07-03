using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models;

namespace BarsGroupProjectN1.Core.Repositories;

public abstract class RepositoryHttpClientBase
{
    protected readonly HttpClient HttpClient;

    protected RepositoryHttpClientBase(string httpClientName, IHttpClientFactory httpClientFactory)
    {
        HttpClient = httpClientFactory.CreateClient(httpClientName);
        HttpClient.DefaultRequestHeaders.Add(UserClaimsStrings.Role, ((int)RoleCode.Admin).ToString());
    }
}