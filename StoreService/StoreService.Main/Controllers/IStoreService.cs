using StoreService.Core;

namespace StoreService.Main.Controllers;

public interface IStoreService
{
    /// <summary>
    /// Calculates the total cooking time for a given list of products at a specific store.
    /// </summary>
    /// <param name="storeId">The ID of the store.</param>
    /// <param name="products">The list of products to be cooked.</param>
    /// <returns>A TimeSpan representing the total cooking time. </returns>
    /// <exception cref="NotFoundException">Thrown if the store cannot prepare any of the requested products or the store is not found.</exception>
    Task<TimeSpan> GetCookingTime(Guid storeId, List<ProductInventory> products);
}