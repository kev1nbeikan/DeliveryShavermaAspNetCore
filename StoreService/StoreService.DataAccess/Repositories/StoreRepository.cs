using Microsoft.EntityFrameworkCore;
using StoreService.Core;
using StoreService.Core.Abstractions;
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
        var storeEntity = await _storeDbContext.Stores.AsNoTracking().FirstOrDefaultAsync(s => s.Id == storeId);
        return storeEntity?.ToCore();
    }

    public async Task<bool> Update(Store store)
    {
        var storeEntity = _storeDbContext.Stores.FirstOrDefault(s => s.Id == store.Id);
        if (storeEntity is null) return false;

        storeEntity.Status = store.Status;

        return await _storeDbContext.SaveChangesAsync() > 0;
    }

    public async Task Add(Store store)
    {
        await _storeDbContext.Stores.AddAsync(store.ToEntity());
        await _storeDbContext.SaveChangesAsync();
    }
}