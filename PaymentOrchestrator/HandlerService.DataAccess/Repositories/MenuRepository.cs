using System.Net.Http.Json;
using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Microsoft.Extensions.Configuration;

namespace HandlerService.DataAccess.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public MenuRepository(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        httpClient.BaseAddress = new Uri(configuration["menuUrl"] ?? throw new Exception("menuUrl not found"));
    }

    public async Task<(Product[] products, string? error)> Get(List<Guid> productIds)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("menu", productIds);

        if (!response.IsSuccessStatusCode) return ([], "Failed to fetch menu data");

        List<Product>? result = await response.Content.ReadFromJsonAsync<List<Product>>();

        return result == null
            ? ([], null)
            : (result.ToArray(), null);
    }
}