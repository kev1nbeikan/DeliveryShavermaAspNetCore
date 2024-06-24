namespace StoreService.Core.Abstractions;

public interface IStoreProductsService
{
    Task<bool> CheckProductsCount(Guid storeId, List<ProductQuantity> requiredProductsQuantities);
}