using BarsGroupProjectN1.Core.Models;

namespace UserService.Core.Abstractions;

public interface IUserRepository
{
    Task<MyUser?> Get(Guid id);
    Task<MyUser> Add(MyUser user);
    Task<bool> Update(MyUser user);
    Task<bool> Update(MyUser user, string newAddress, string newComment, string newPhoneNumber);
}