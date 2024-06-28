namespace StoreService.Core.Abstractions;

public interface IStoreServiceMenuRepository
{
    Task<List<Product>> GetAll();
}