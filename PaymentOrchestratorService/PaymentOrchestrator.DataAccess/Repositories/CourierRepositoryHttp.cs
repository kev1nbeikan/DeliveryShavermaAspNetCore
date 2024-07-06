using System.Net.Http.Json;
using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Courier;
using BarsGroupProjectN1.Core.Repositories;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Common;
using HandlerService.DataAccess.Contracts;
using HandlerService.Infustucture;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HandlerService.DataAccess.Repositories;

public class CourierRepositoryHttp : RepositoryHttpClientBase, ICourierRepository
{
    public CourierRepositoryHttp(IHttpClientFactory httpClientFactory, IOptions<ServicesOptions> options) : base(
        nameof(options.Value.CouriersUrl), httpClientFactory)
    {
    }

    public async Task<(OrderTaskExecution<Courier>?, string? error)> GetDeliveryExecution(string clientAddress,
        string storeAddress)
    {
        var requestUri = new UriBuilder()
        {
            Query = QueryUtils.GetQueryString(clientAddress, storeAddress),
            Path = "api/courier/find"
        }.Uri.PathAndQuery;


        HttpResponseMessage response = await HttpClient.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode) return (null, "Ошибка при получении данных курьера. " + await response.Content.ReadAsStringAsync());
        OrderTaskExecution<Courier>? result =
            await response.Content.ReadFromJsonAsync<OrderTaskExecution<Courier>>();

        return result == null
            ? (null, "Ошибка получения данных курьера")
            : (result, null);
    }
}