using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarsGroupProjectN1.Core.Models.Payment;

namespace StoreService.Core.Abstractions;

/// <summary>
/// Сервис управления продуктами в магазине.
/// </summary>
public interface IStoreProductsService
{
    /// <summary>
    /// Проверяет наличие достаточного количества продуктов для заказа.
    /// </summary>
    /// <param name="storeId">Идентификатор магазина.</param>
    /// <param name="requiredProductsQuantities">Список требуемых количеств продуктов.</param>
    Task<bool> CheckProductsCount(Guid storeId, List<ProductInventoryWithName> requiredProductsQuantities);
    /// <summary>
    /// Добавляет или обновляет информацию о количестве продукта в магазине.
    /// </summary>
    /// <param name="storeId">Идентификатор магазина.</param>
    /// <param name="productId">Идентификатор продукта.</param>
    /// <param name="quantity">Количество для добавления или вычета из инвентаря продукта.</param>
    /// <remarks>
    /// Если продукт не найден в магазине, он будет добавлен с указанным количеством.
    /// Если продукт найден, его инвентарь будет обновлен новым количеством.
    /// </remarks>
    Task UpsertProductInventory(Guid storeId, Guid productId, int quantity);
    Task<ProductInventory> GetById(Guid storeId, Guid productId);
    Task<List<ProductInventory>> GetAll(Guid storeId);
}