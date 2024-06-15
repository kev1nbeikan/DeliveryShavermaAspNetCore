namespace AuthService.Core.Abstractions;

public interface IUserAuthRepo
{
    public Task Add(UserAuth user);
    public Task<UserAuth?> GetByEmail(string email);
    public Task<UserAuth?> GetById(Guid id);
}