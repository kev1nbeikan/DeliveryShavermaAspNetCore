using BarsGroupProjectN1.Core.Models.Store;
using Microsoft.EntityFrameworkCore;
using StoreService.Core;
using StoreService.Core.Abstractions;
using StoreService.Core.Exceptions;
using StoreService.DataAccess.Extensions;

namespace StoreService.DataAccess.Repositories;

public class StoreRepository : IStoreRepository
{
    private readonly StoreDbContext _storeDbContext;

    public StoreRepository(StoreDbContext storeDbContext)
    {
        _storeDbContext = storeDbContext;
    }

    public async Task<Store?> Get(Guid storeId)
    {
        var storeEntity = await _storeDbContext.Stores
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == storeId);
        return storeEntity?.ToCore();
    }

    public async Task<bool> Update(Store store)
    {
        var storeEntity = await _storeDbContext.Stores.FirstOrDefaultAsync(s => s.Id == store.Id);
        if (storeEntity is null) return false;

        storeEntity.Status = store.Status;
        storeEntity.ActiveOrdersCount = store.ActiveOrdersCount;

        return await _storeDbContext.SaveChangesAsync() > 0;
    }

    public async Task<StoreStatus?> GetStatus(Guid storeId)
    {
        var store = await _storeDbContext.Stores.FirstOrDefaultAsync(s => s.Id == storeId);
        return store?.Status;
    }

    public async Task<List<Store>> GetAll()
    {
        return await _storeDbContext.Stores.AsNoTracking().Select(s => s.ToCore()).ToListAsync();
    }

    public Task<List<Store>> GetAllOpened()
    {
        return _storeDbContext.Stores
            .AsNoTracking()
            .Where(s => s.Status == StoreStatus.Open)
            .Select(s => s.ToCore())
            .ToListAsync();
    }

    public async Task Add(Store store)
    {
        await EnsureValidToAdd(store);
        await _storeDbContext.Stores.AddAsync(store.ToEntity());
        await _storeDbContext.SaveChangesAsync();
    }

    private async Task EnsureValidToAdd(Store store)
    {
        if (await Get(store.Id) is not null)
        {
            throw new DuplicateEntryException<Store>(
                "You can add the store with the same id",
                nameof(StoreRepository),
                store
            );
        }
    }
}