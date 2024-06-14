using System.Net.Http.Json;
using Handler.Core.Abstractions.Repositories;
using HandlerService.Controllers;
using HandlerService.DataAccess.Contracts;
using Microsoft.Extensions.Configuration;

namespace HandlerService.DataAccess.Repositories;

public class CurierRepository : ICurierRepository
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CurierRepository(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        httpClient.BaseAddress = new Uri(configuration["curiersUrl"] ?? throw new Exception("curiersUrl not found"));
    }


    public async Task<(Curier?, TimeSpan)> GetCurierAsync(string resource)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(resource);

        if (response.IsSuccessStatusCode)
        {
            CurierWithDeliveryTimeResponse? result =
                await response.Content.ReadFromJsonAsync<CurierWithDeliveryTimeResponse>();

            return result == null
                ? (null, TimeSpan.Zero)
                : (result.Curier, result.DeliveryTime);
        }

        return (null, TimeSpan.Zero);
    }

    public async Task<(Curier?, TimeSpan deliveryTime)> GetCurier(string clientAddress)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"curiers/find?address={clientAddress}");

        if (!response.IsSuccessStatusCode) return (null, TimeSpan.Zero);
        CurierWithDeliveryTimeResponse? result =
            await response.Content.ReadFromJsonAsync<CurierWithDeliveryTimeResponse>();

        return result == null
            ? (null, TimeSpan.Zero)
            : (result.Curier, result.DeliveryTime);
    }
}