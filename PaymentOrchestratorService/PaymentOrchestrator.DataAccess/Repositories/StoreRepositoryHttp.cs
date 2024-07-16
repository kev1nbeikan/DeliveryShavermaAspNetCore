using System.Net.Http.Json;
using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Payment;
using BarsGroupProjectN1.Core.Models.Store;
using BarsGroupProjectN1.Core.Repositories;
using Handler.Core.Abstractions.Repositories;
using Microsoft.Extensions.Options;

namespace HandlerService.DataAccess.Repositories;

public class StoreRepositoryHttp : RepositoryHttpClientBase, IStoreRepository
{
    public StoreRepositoryHttp(IHttpClientFactory httpClientFactory, IOptions<ServicesOptions> options) :
        base(nameof(options.Value.StoreUrl), httpClientFactory)
    {
    }

    public async Task<(OrderTaskExecution<Store>? cookingExecution, string? error)> GetCokingTime(string clientAddress,
        List<ProductInventoryWithName> basket)
    {
        try
        {

            var body = new GetCookingTimeRequest(clientAddress, basket);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                Content = JsonContent.Create(body),
                RequestUri = new Uri("store/api/v1.0/get-cooking-info", UriKind.Relative)
            };

            var response = await HttpClient.SendAsync(request);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            if (!response.IsSuccessStatusCode) return (null, await response.Content.ReadAsStringAsync());

            var cookingExecution = await response.Content.ReadFromJsonAsync<OrderTaskExecution<Store>>();

            return (cookingExecution, null);
        }
        catch (HttpRequestException e)
        {
            return (null, e.Message);
        }
    }
}