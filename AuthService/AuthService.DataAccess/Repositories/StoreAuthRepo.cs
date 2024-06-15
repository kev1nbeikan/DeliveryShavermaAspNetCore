using AuthService.Core.Abstractions;
using AuthService.DataAccess.Entities;
using AuthService.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Repositories;

public class StoreAuthRepo : IStoreAuthRepo
{
    private readonly AuthDbContext _dbContext;


    public StoreAuthRepo(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task Add(StoreAuth store)
    {
        _dbContext.StoreAuth.Add(store.ToEntity());
        return _dbContext.SaveChangesAsync();
    }

    public async Task<StoreAuth?> GetByLogin(string login)
    {
        var storeEntity = await _dbContext.StoreAuth
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == login);

        return storeEntity?.ToCore();
    }

    public async Task<StoreAuth?> GetById(Guid id)
    {
        var storeAuthEntity = await _dbContext.StoreAuth.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return storeAuthEntity?.ToCore();
    }
}