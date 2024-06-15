using AuthService.Core;
using AuthService.Core.Abstractions;
using AuthService.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Repositories;

public class UserAuthRepo : IUserAuthRepo
{
    private readonly AuthDbContext _dbContext;


    public UserAuthRepo(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task Add(UserAuth user)
    {
        _dbContext.UsersAuth.Add(user.ToEntity());
        return _dbContext.SaveChangesAsync();
    }


    public async Task<UserAuth?> GetByEmail(string email)
    {
        var userEntity = await _dbContext.UsersAuth
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);

        return userEntity?.ToCore();
    }


    public async Task<UserAuth?> GetById(Guid id)
    {
        var userAuthEntity = await _dbContext.UsersAuth.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return userAuthEntity?.ToCore();
    }
}