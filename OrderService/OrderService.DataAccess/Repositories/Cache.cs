using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Models;

namespace OrderService.DataAccess.Repositories;

public class Cashe(IDistributedCache cache) : ICache
{
    private readonly IDistributedCache _cache = cache;

    public async Task SetAsync<T>(string key, T value) =>
        await _cache.SetStringAsync(key, JsonConvert.SerializeObject(value));


    public async Task<CacheResult<T>> GetAsync<T>(string key)
    {
        var value = await _cache.GetStringAsync(key);

        return string.IsNullOrEmpty(value)
            ? CacheResult<T>.NotFound()
            : CacheResult<T>.Found(JsonConvert.DeserializeObject<T>(value)!);
    }

    public Task RemoveAsync(string key) => _cache.RemoveAsync(key);
}