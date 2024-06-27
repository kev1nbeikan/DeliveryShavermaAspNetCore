using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using BarsGroupProjectN1.Core.AppSettings;
using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Common;
using HandlerService.Infustucture;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HandlerService.DataAccess.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly HttpClient _httpClient;

    public MenuRepository(IHttpClientFactory httpClientFactory, IOptions<ServicesOptions> options)
    {
        _httpClient = httpClientFactory.CreateClient(nameof(options.Value.MenuUrl));
    }

    public async Task<(Product[] products, string? error)> Get(List<Guid> productIds)
    {
        var requestUri = new UriBuilder()
        {
            Query = QueryUtils.GetQueryString(productIds),
            Path = "api/product/getproductsbyid"
        }.Uri.PathAndQuery;

        HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode) return ([], "Failed to fetch menu data");

        List<Product>? result = await response.Content.ReadFromJsonAsync<List<Product>>();

        return result == null
            ? ([], null)
            : (result.ToArray(), null);
    }
}