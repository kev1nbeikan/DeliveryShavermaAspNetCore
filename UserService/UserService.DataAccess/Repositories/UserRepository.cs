using Microsoft.EntityFrameworkCore;
using UserService.Core;
using UserService.Core.abstractions;
using UserService.Core.Exceptions;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Extentions;

namespace UserService.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _userDbContext;

    public UserRepository(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }


    public async Task<Guid> Save(MyUser user)
    {
        await _userDbContext.Users.AddAsync(user.ToUserEntity());
        await _userDbContext.SaveChangesAsync();
        return user.UserId;
    }

    public async Task<bool> Update(MyUser user)
    {
        var userEntity = user.ToUserEntity();

        _userDbContext.Users.Update(userEntity);
        return await _userDbContext.SaveChangesAsync() > 0;
    }

    public async Task<MyUser?> Get(Guid id)
    {
        var result = await _userDbContext.Users.AsNoTracking().Where(x => x.Id == id)
            .Include(x => x.Addresses)
            .FirstOrDefaultAsync();
        if (result == null)
        {
            throw new NotFoundException("User not found");
        }

        return result?.ToUserEntity();
    }
}