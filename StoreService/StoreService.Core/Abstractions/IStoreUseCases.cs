namespace StoreService.Core.Abstractions;

public interface IStoreUseCases
{
    Task<List<MenuProductInventory>> GetMenuProductsWithInventory(Guid storeId);
}