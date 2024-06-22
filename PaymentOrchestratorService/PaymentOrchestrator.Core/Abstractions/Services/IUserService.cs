using Handler.Core.Common;

namespace Handler.Core.Abstractions.Services;

public interface IUserService
{
    Task<string?> Upsert(Guid userId, string address, string comment, string phoneNumber);
    Task<(MyUser?, string? error)> Get(Guid userId);
}