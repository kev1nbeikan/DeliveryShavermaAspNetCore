using AuthService.DataAccess.Entities;

namespace AuthService.Core.Abstractions;

public interface IStoreAuthService
{
    public Task<Guid> Login(string login, string password);

    public Task<Guid> Register(string login, string password);

    public Task<StoreAuth> GetStore(Guid id);

}