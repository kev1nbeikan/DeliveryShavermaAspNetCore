using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Common;
using Handler.Core.Payment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HandlerService.DataAccess.Repositories;

public class StoreRepository : RepositoryHttpClientBase, IStoreRepository
{
    public StoreRepository(IHttpClientFactory httpClientFactory, IOptions<ServicesOptions> options) :
        base(nameof(options.Value.StoreUrl), httpClientFactory)
    {
    }

    public async Task<(TimeSpan cookingTime, string? error)> GetCokingTime(Guid storeId, List<BucketItem> basket)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            Content = JsonContent.Create(basket),
            RequestUri = new Uri($"store/api/v1.0/cookingtime/{storeId}", UriKind.Relative)
        };


        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode) return (TimeSpan.Zero, await response.Content.ReadAsStringAsync());

        var responseContent = await response.Content.ReadAsStringAsync();
        var cookingTime = TimeSpan.Parse(responseContent);
        return (cookingTime, null);
    }
}