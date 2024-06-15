namespace AuthService.Core.Abstractions;

public interface IUserAuthService
{
    public Task<UserAuth> Register(string userName, string email, string passwordHash);
    public Task<Guid> Login(string email, string password);
    public Task<UserAuth> GetUser(Guid id);
}