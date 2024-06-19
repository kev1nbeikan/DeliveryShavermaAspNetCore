using System.Net.Http.Json;
using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Common;
using Handler.Core.Payment;
using HandlerService.DataAccess.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HandlerService.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HttpClient _httpClient;

    public UserRepository(IOptions<ServicesOptions> options)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(options.Value.UsersUrl ?? throw new Exception("userUrl not found"));
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

    public async Task<string?> Save(MyUser user)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/users/AddNewOrUpdate", user);

        return response.IsSuccessStatusCode
            ? null
            : "user not saved";
    }

    public async Task<string?> Save(Guid userId, List<BucketItem> productIdsAndQuantity, string comment, string address,
        string phoneNumber, string storeId)
    {
        AddNewUserOrUpdateRequest request = new(userId, productIdsAndQuantity, comment, address, phoneNumber, storeId);

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/users/AddNewOrUpdate", request);

        return response.IsSuccessStatusCode
            ? null
            : "user not saved";
    }
}