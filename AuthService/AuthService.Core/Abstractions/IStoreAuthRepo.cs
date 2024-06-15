
using AuthService.DataAccess.Entities;

namespace AuthService.Core.Abstractions;

public interface IStoreAuthRepo
{

    public Task Add(StoreAuth store);
    public Task<StoreAuth?> GetByLogin(string login);
    public Task<StoreAuth?> GetById(Guid id);
    
}