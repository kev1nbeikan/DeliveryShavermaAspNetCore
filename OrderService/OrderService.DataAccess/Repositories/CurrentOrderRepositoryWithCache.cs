using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using Microsoft.Extensions.Logging;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Models;

namespace OrderService.DataAccess.Repositories;

public class CurrentOrderRepositoryWithCache(
    CurrentOrderRepository repository,
    ICache cache,
    ILogger<CurrentOrderRepositoryWithCache> logger) : ICurrentOrderRepository
{
    readonly ICache _cache = cache;
    readonly ICurrentOrderRepository _repository = repository;
    readonly ILogger<CurrentOrderRepositoryWithCache> _logger = logger;

    private async Task<T> GetFromCacheOrDataBase<T>(string key, Func<Task<T>> getDataFromDb)
    {
        var cachedResult = await _cache.GetAsync<T>(key);

        if (cachedResult.IsFound)
        {
            _logger.LogInformation("Запрос из кэша получен. Key = {Key}", key);
            return cachedResult.Value!;
        }

        var result = await getDataFromDb();
        _logger.LogInformation("Запрос из базы получен. Key = {Key}", key);
        await _cache.SetAsync(key, result);
        return result;
    }

    private async Task RefreshCache(string key)
    {
        _logger.LogInformation("Обновление кэша. Key = {Key}", key);
        await _cache.RemoveAsync(key);
    }


    public async Task ChangeStatus(RoleCode role, StatusCode status, Guid sourceId, Guid id)
    {
        await repository.ChangeStatus(role, status, sourceId, id);
        await RefreshCache(GenerateKey(id.ToString()));
    }

    public async Task<StatusCode> GetStatus(RoleCode role, Guid sourceId, Guid id)
        => await GetFromCacheOrDataBase(
            GenerateKey(id.ToString()),
            () =>
                _repository.GetStatus(role, sourceId, id)
        );


    private static string GenerateKey(params string[] args)
    {
        return string.Join("_", args);
    }

    public async Task<List<CurrentOrder>> Get(RoleCode role, Guid sourceId)
        => await repository.Get(role, sourceId);

    public async Task<CurrentOrder> GetById(RoleCode role, Guid sourceId, Guid id)
        => await repository.GetById(role, sourceId, id);

    public async Task Create(CurrentOrder order)
        => await repository.Create(order);

    public async Task Delete(RoleCode role, Guid sourceId, Guid id)
    {
        await repository.Delete(role, sourceId, id);
        await RefreshCache(GenerateKey(id.ToString()));
    }

    public async Task ChangeCookingDate(RoleCode role, DateTime cookingDate, Guid sourceId, Guid id)
        => await repository.ChangeCookingDate(role, cookingDate, sourceId, id);

    public async Task ChangeDeliveryDate(RoleCode role, DateTime deliveryDate, Guid sourceId, Guid id)
        => await repository.ChangeDeliveryDate(role, deliveryDate, sourceId, id);
}