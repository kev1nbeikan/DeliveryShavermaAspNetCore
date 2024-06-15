using AuthService.Core;
using AuthService.Core.Abstractions;
using AuthService.DataAccess.Entities;
using AuthService.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Repositories;

public class CurierAuthRepo : ICurierAuthRepo
{
    private readonly AuthDbContext _dbContext;

    public CurierAuthRepo(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task Add(CurierAuth curier)
    {
        _dbContext.CurierAuth.Add(curier.ToEntity());
        return _dbContext.SaveChangesAsync();
    }

    public async Task<CurierAuth?> GetByLogin(string login)
    {
        var curierEntity = await _dbContext.CurierAuth
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == login);

        return curierEntity?.ToCore();
    }

    public async Task<CurierAuth?> GetById(Guid id)
    {
        var curierAuthEntity = await _dbContext.CurierAuth.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return curierAuthEntity?.ToCore();
    }
}