using Handler.Core.Common;

namespace Handler.Core.Abstractions.Services;

public interface IUserService
{
    Task<string?> AddNewOrUpdate(Guid userId, string address, string comment, string phoneNumber);
    Task<(MyUser?, string? error)> Get(Guid userId);
}