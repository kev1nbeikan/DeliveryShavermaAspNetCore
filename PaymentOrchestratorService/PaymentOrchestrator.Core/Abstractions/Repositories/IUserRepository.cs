using Handler.Core.Common;

namespace Handler.Core.Abstractions;

public interface IUserRepository
{
    Task<MyUser?> Get(Guid userId);
    Task<string?> Save(MyUser user);
}