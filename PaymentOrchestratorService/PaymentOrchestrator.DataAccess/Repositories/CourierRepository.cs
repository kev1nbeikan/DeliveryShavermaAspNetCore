using System.Net.Http.Json;
using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Repositories;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Common;
using HandlerService.DataAccess.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HandlerService.DataAccess.Repositories;

public class CourierRepository : RepositoryHttpClientBase, ICurierRepository
{
    public CourierRepository(IHttpClientFactory httpClientFactory, IOptions<ServicesOptions> options) : base(
        nameof(options.Value.CouriersUrl), httpClientFactory)
    {
    }

    public async Task<(Curier?, TimeSpan deliveryTime)> GetCourier(string clientAddress)
    {
        HttpResponseMessage response = await HttpClient.GetAsync($"curiers/find?address={clientAddress}");

        if (!response.IsSuccessStatusCode) return (null, TimeSpan.Zero);
        CurierWithDeliveryTimeResponse? result =
            await response.Content.ReadFromJsonAsync<CurierWithDeliveryTimeResponse>();

        return result == null
            ? (null, TimeSpan.Zero)
            : (result.Curier, result.DeliveryTime);
    }
}