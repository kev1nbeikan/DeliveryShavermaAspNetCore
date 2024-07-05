using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using BarsGroupProjectN1.Core.Models.Payment;
using BarsGroupProjectN1.Core.Models.Store;
using StoreService.Core.Exceptions;

namespace StoreService.Core.Abstractions;

public interface IStoreService
{
    /// <summary>
    /// Получает время приготовления продуктов на основе адреса клиента. Сначала находит ближайший магазин к адресу, проверяет наиличе товаров, затем уже высчитывает время готовки
    /// </summary>
    /// <param name="clientAddress">Адрес клиента.</param>
    /// <param name="products">Список продуктов.</param>
    /// <returns>Объект OrderTaskExecution, содержащий магазин и время приготовления.</returns>
    /// <exception cref="StoreNotFoundException">Магазин не найден.</exception>
    /// <exception cref="UnavailableProductsException">Товары не найдены.</exception>
    /// <exception cref="StoreClosedException">Товары не найдены.</exception>
    Task<OrderTaskExecution<Store>> GetCookingInfo(string storeId, List<ProductsInventory> products);

    Task<StoreStatus> GetStatus(Guid storeId);

    /// <summary>
    /// Получает существующий магазин по идентификатору или добавляет новый закрытый магазин без адреса.
    /// </summary>
    /// <param name="storeId">Идентификатор магазина.</param>
    /// <returns>Магазин, который был получен или добавлен.</returns>
    /// <exception cref="ArgumentException">Бросается, если идентификатор магазина недопустим.</exception>
    Task<Store> GetOrAddNewStore(Guid storeId);

    Task<bool> UpdateStore(Guid storeId,
        string address);

    Task UpdateStatus(Guid storeId, StoreStatus newStatus);
    Task AdjustActiveOrdersCount(Guid storeId, int adjustment = 1);
    Task OnOrderCreate(PublishOrder order);
    Task OnOrderUpdate(PublishOrder order);
}