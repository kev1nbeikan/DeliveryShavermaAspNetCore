using System.Net.Http.Json;
using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Repositories;
using Microsoft.Extensions.Options;
using StoreService.Application;
using StoreService.Core;

namespace StoreService.DataAccess.Repositories;

public class StoreServiceSpecificMenuRepository(IHttpClientFactory httpClientFactory, IOptions<ServicesOptions> options)
    : RepositoryHttpClientBase(options.Value.MenuUrl, httpClientFactory), IStoreServiceSpecificMenuRepository
{
    public Task<List<Product>> GetAll()
    {
        _httpClient.GetFromJsonAsync<List<Product>>()
    }
}