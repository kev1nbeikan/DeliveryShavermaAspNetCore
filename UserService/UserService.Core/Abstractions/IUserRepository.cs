namespace UserService.Core.Abstractions;

public interface IUserRepository
{
    Task<MyUser?> Get(Guid id);
    Task<MyUser> Add(MyUser user);
    Task<bool> Update(MyUser user);
}