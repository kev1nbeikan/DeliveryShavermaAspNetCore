using System.Net.Http.Json;
using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Exceptions;
using BarsGroupProjectN1.Core.Repositories;
using Microsoft.Extensions.Options;
using StoreService.Core;
using StoreService.Core.Abstractions;

namespace StoreService.DataAccess.Repositories;

public class StoreServiceMenuRepository(IHttpClientFactory httpClientFactory, IOptions<ServicesOptions> options)
    : RepositoryHttpClientBase(nameof(options.Value.MenuUrl), httpClientFactory), IStoreServiceMenuRepository
{
    public async Task<List<Product>> GetAll()
    {
        const string path = "/api/product/";

        try
        {
            var response = await HttpClient.GetAsync(path);
            if (!response.IsSuccessStatusCode) ExecuteException(path);

            return await response.Content.ReadFromJsonAsync<List<Product>>() ?? [];
        }
        catch (HttpRequestException e)
        {
            ExecuteException(e.Message);
            return [];
        }
    }

    private void ExecuteException(string path, string message = "")
    {
        throw new RemoteServiceException("MenuService",
            HttpClient.BaseAddress + path,
            "Обратитесь к администратору для получения подробностей", message);
    }
}