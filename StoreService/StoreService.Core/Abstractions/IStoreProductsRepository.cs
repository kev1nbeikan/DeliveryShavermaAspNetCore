namespace StoreService.Core.Abstractions;

public interface IStoreProductsRepository
{
    Task<List<ProductQuantity>> GetProductsQuantiies(Guid storeId, List<ProductQuantity> products);
}