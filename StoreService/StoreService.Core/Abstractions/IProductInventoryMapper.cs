namespace StoreService.Core.Abstractions;

public interface IProductInventoryMapper
{
    Task<List<MappedProduct>> GetMappedProducts(Guid storeId);
}