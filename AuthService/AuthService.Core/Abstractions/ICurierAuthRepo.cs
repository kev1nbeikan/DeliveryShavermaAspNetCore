namespace AuthService.Core.Abstractions;

public interface ICurierAuthRepo
{
    public Task Add(CurierAuth curier);
    public Task<CurierAuth?> GetByLogin(string login);
    public Task<CurierAuth?> GetById(Guid id);
}