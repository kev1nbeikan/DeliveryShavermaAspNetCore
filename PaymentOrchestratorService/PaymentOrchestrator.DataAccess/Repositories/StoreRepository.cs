using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Store;
using BarsGroupProjectN1.Core.Repositories;
using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Common;
using Handler.Core.Payment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using GetCookingTimeRequest = HandlerService.DataAccess.Contracts.GetCookingTimeRequest;

namespace HandlerService.DataAccess.Repositories;

public class StoreRepository : RepositoryHttpClientBase, IStoreRepository
{
    public StoreRepository(IHttpClientFactory httpClientFactory, IOptions<ServicesOptions> options) :
        base(nameof(options.Value.StoreUrl), httpClientFactory)
    {
    }

    public async Task<(OrderTaskExecution<Store>? cookingExecution, string? error)> GetCokingTime(string clientAddress,
        List<BucketItem> basket)
    {
        var body = new GetCookingTimeRequest(clientAddress, basket);

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            Content = JsonContent.Create(body),
            RequestUri = new Uri("store/api/v1.0/get-execution-info", UriKind.Relative)
        };

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode) return (null, await response.Content.ReadAsStringAsync());

        var cookingExecution = await response.Content.ReadFromJsonAsync<OrderTaskExecution<Store>>();
        return (cookingExecution, null);
    }
}