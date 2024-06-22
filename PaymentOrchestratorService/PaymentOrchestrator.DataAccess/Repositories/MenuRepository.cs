using System.Net.Http.Json;
using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HandlerService.DataAccess.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly HttpClient _httpClient;

    public MenuRepository(IHttpClientFactory httpClientFactory, IOptions<ServicesOptions> options)
    {
        _httpClient = httpClientFactory.CreateClient(options.Value.MenuUrl);
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