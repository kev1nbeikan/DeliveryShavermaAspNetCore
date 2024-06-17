using System.Net.Http.Json;
using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Common;
using Microsoft.Extensions.Configuration;

namespace HandlerService.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public UserRepository(IConfiguration configuration)
    {
        _configuration = configuration;

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(configuration["usersUrl"] ?? throw new Exception("userUrl not found"));
    }

    public async Task<MyUser?> Get(Guid userId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"/users/{userId}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<MyUser>();
        }

        return null;
    }

    public async Task<string?> SaveByUserId(MyUser user)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/users", user);

        return response.IsSuccessStatusCode
            ? null
            : "user not saved";
    }
}