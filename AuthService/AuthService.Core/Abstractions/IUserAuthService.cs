namespace AuthService.Core.Abstractions;

public interface IUserAuthService
{
    public Task<Guid> Register(string email, string password);
    public Task<Guid> Login(string email, string password);
    public Task<UserAuth> GetUser(Guid id);
}