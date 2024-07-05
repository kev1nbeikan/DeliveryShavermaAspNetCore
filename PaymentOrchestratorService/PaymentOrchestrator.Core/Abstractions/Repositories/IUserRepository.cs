using Handler.Core.Contracts;
using UserService.Core;

namespace Handler.Core.Abstractions.Repositories;

public interface IUserRepository
{
    Task<MyUser?> Get(Guid userId);
    Task<string?> Upsert(UpsertFields fields);
}