using Handler.Core.Common;
using Handler.Core.Contracts;

namespace Handler.Core.Abstractions.Repositories;

public interface IUserRepository
{
    Task<MyUser?> Get(Guid userId);
    Task<string?> Save(MyUser user);
    Task<string?> Upsert(UpsertFields fields);
}