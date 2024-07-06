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
    /// Возвращает магазин, если такого нет, то созадет новый с заданным идентификатором и статусом "закрыто" и возвращает его
    /// </summary>
    /// <param name="storeId">Идентификатор магазина.</param>
    /// <returns>Объект магазина.</returns>
    /// <exception cref="ArgumentException">Если заданные параметры магазина не валидны.</exception>
    Task<Store> GetOrAddNewStore(Guid storeId);

    Task<bool> UpdateStore(Guid storeId,
        string address);
    
    /// <summary>
    /// Обновляет статус магазина.
    /// </summary>
    /// <param name="storeId">Идентификатор магазина.</param>
    /// <param name="newStatus">Новый статус магазина.</param>
    /// <exception cref="StoreNotFoundException">Если магазин с указанным идентификатором не найден.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Если указан неизвестный статус магазина.</exception>
    /// <exception cref="ArgumentException">Если магазин не может менять статус, так как имеются активные заказы или у магазина нет полной информации (адресс)</exception>
    Task UpdateStatus(Guid storeId, StoreStatus newStatus);
    Task AdjustActiveOrdersCount(Guid storeId, int adjustment = 1);
    Task OnOrderCreate(PublishOrder order);
    Task OnOrderUpdate(PublishOrder order);
}