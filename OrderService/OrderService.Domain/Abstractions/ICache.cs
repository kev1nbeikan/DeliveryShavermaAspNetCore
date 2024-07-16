using OrderService.Domain.Models;

namespace OrderService.Domain.Abstractions;

public interface ICache
{
    public Task SetAsync<T>(string key, T value);


    public Task<CacheResult<T>> GetAsync<T>(string key);

    public Task RemoveAsync(string key);
}