namespace StoreService.Core.Abstractions;

public interface IProductInventoryMapper
{
    /// <summary>
    /// Получает список продуктов из меню и находит количество каждого продукта в инвентаре указанного магазина.
    /// </summary>
    /// <param name="storeId">Идентификатор магазина.</param>
    /// <returns>Список продуктов с указанием количества каждого продукта в инвентаре магазина.</returns>
    Task<List<MappedProduct>> GetMappedProducts(Guid storeId);
}