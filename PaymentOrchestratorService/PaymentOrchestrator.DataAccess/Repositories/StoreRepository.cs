using System.Text;
using System.Text.Json;
using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Common;
using Microsoft.Extensions.Configuration;

namespace HandlerService.DataAccess.Repositories;

public class StoreRepository : IStoreRepository
{
    private readonly HttpClient _httpClient;

    public StoreRepository(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration["StoreUrl"] ?? throw new Exception("StoreUrl not found"));
    }

    public async Task<(TimeSpan cookingTime, string? error)> GetCokingTime(Guid storeId, Product[] basket)
    {
        var json = JsonSerializer.Serialize(basket);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(_httpClient.BaseAddress!.PathAndQuery + "/cooking-time/" + storeId),

            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode) return (TimeSpan.Zero, "Error fetching cooking time");

        var responseContent = await response.Content.ReadAsStringAsync();
        var cookingTime = TimeSpan.Parse(responseContent);
        return (cookingTime, null);
    }
}