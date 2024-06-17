using Handler.Core.Common;

namespace Handler.Core.Abstractions;

public interface IUserService
{
    Task<string?> Save(Guid userId, string address, string comment);
    Task<(MyUser?, string? error)> Get(Guid userId);
}