namespace StoreService.Core.Abstractions;

public interface IMenuRepositoryApi
{
    Task<List<Product>> GetAll();
}