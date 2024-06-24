using StoreService.Core;
using StoreService.Core.Abstractions;
using StoreService.Core.Exceptions;

namespace StoreService.Main.Controllers;

public interface IStoreService
{
    /// <summary>
    /// Returns the estimated cooking time for an order, 
    /// based on the products and the store.
    /// </summary>
    /// <param name="storeId">The ID of the store.</param>
    /// <param name="products">The list of products that are being ordered.</param>
    /// <returns>The estimated cooking time.</returns>
    /// <exception cref="StoreServiceException">
    /// Thrown when there is an error related to the store or its products.
    /// This can be due to the store not being found, the store being closed, 
    /// or some of the products being unavailable.
    /// Specific exceptions inherited from StoreServiceException include:
    ///     <see cref="StoreNotFoundException"/>
    ///     <see cref="StoreClosedException"/>
    ///     <see cref="UnavailableProductsException"/>
    /// </exception>
    Task<TimeSpan> GetCookingTime(Guid storeId, List<ProductInventory> products);
}