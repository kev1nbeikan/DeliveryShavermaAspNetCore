using System.Net.Http.Json;
using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Repositories;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Common;
using HandlerService.Infustucture;
using Microsoft.Extensions.Options;

namespace HandlerService.DataAccess.Repositories;

public class MenuRepository : RepositoryHttpClientBase, IMenuRepository
{
    public MenuRepository(IHttpClientFactory httpClientFactory, IOptions<ServicesOptions> options) : base(
        nameof(options.Value.MenuUrl), httpClientFactory)
    {
    }

    public async Task<(Product[] products, string? error)> Get(List<Guid> productIds)
    {
        var requestUri = new UriBuilder()
        {
            Query = QueryUtils.GetQueryString(productIds),
            Path = "api/product/getproductsbyid"
        }.Uri.PathAndQuery;

        HttpResponseMessage response = await HttpClient.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode) return ([], "Невозможно получить список продуктов");

        List<Product>? result = await response.Content.ReadFromJsonAsync<List<Product>>();

        return result == null
            ? ([], null)
            : (result.ToArray(), null);
    }
}