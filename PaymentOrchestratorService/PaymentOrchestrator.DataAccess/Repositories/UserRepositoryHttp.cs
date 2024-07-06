using System.Net.Http.Json;
using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Repositories;
using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Common;
using Handler.Core.Contracts;
using Microsoft.Extensions.Options;
using UserService.Core;

namespace HandlerService.DataAccess.Repositories;

public class UserRepositoryHttp : RepositoryHttpClientBase, IUserRepository
{
    public UserRepositoryHttp(IOptions<ServicesOptions> options, IHttpClientFactory httpClientFactory) :
        base(nameof(options.Value.UsersUrl), httpClientFactory)
    {
    }

    public async Task<MyUser?> Get(Guid userId)
    {
        HttpResponseMessage response = await HttpClient.GetAsync($"/user/{userId}");

        Console.WriteLine(await response.Content.ReadAsStringAsync());

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<MyUser>();
        }

        return null;
    }

    public async Task<string?> Upsert(UpsertFields fields)
    {
        var httpRequest = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = JsonContent.Create(fields),
            RequestUri = new Uri("/user/upsert", UriKind.Relative),
        };

        HttpResponseMessage response = await HttpClient.SendAsync(httpRequest);
        var content = await response.Content.ReadAsStringAsync();

        return string.IsNullOrEmpty(content)
            ? null
            : content;
    }
}