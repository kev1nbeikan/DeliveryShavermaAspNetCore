using AuthService.Core;
using AuthService.Core.Abstractions;
using AuthService.DataAccess.Entities;
using AuthService.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Repositories;

public class CourierAuthRepo : ICourierAuthRepo
{
    private readonly AuthDbContext _dbContext;

    public CourierAuthRepo(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task Add(CourierAuth courier)
    {
        _dbContext.CurierAuth.Add(courier.ToEntity());
        return _dbContext.SaveChangesAsync();
    }

    public async Task<CourierAuth?> GetByLogin(string login)
    {
        var curierEntity = await _dbContext.CurierAuth
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == login);

        return curierEntity?.ToCore();
    }

    public async Task<CourierAuth?> GetById(Guid id)
    {
        var curierAuthEntity = await _dbContext.CurierAuth.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return curierAuthEntity?.ToCore();
    }
}