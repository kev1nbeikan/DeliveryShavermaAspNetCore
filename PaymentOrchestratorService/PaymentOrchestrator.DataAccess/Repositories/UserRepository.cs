using System.Net.Http.Json;
using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Common;
using Handler.Core.Contracts;
using Microsoft.Extensions.Options;

namespace HandlerService.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HttpClient _httpClient;

    public UserRepository(IOptions<ServicesOptions> options, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(nameof(options.Value.UsersUrl));
    }

    public async Task<MyUser?> Get(Guid userId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"/user/{userId}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<MyUser>();
        }

        return null;
    }

    public async Task<string?> Save(MyUser user)
    {
        throw new NotImplementedException();
    }

    public async Task<string?> Upsert(UpsertFields fields)
    {
        var httpRequest = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = JsonContent.Create(fields),
            RequestUri = new Uri("/user/AddNewOrUpdate/AddNewOrUpdate", UriKind.Relative)
        };


        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/user/AddNewOrUpdate/AddNewOrUpdate", fields);
        return response.IsSuccessStatusCode
            ? null
            : await response.Content.ReadAsStringAsync();
    }
}