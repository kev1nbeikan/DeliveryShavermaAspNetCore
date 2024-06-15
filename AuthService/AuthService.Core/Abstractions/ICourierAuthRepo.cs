namespace AuthService.Core.Abstractions;

public interface ICourierAuthRepo
{
    public Task Add(CourierAuth courier);
    public Task<CourierAuth?> GetByLogin(string login);
    public Task<CourierAuth?> GetById(Guid id);
}