namespace UserService.Core.abstractions;

public interface IUserRepository
{
    Task<MyUser?> Get(Guid id);
    Task<Guid> Save(MyUser user);
    Task<bool> Update(MyUser user);
}