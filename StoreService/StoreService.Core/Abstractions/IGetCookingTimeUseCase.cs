using System;
using System.Collections.Generic;
using BarsGroupProjectN1.Core.Models.Payment;

namespace StoreService.Core.Abstractions;

public interface IGetCookingTimeUseCase
{
    /// <summary>
    /// Получить общее время приготовления для указанного магазина и списка товаров с их количествами.
    /// </summary>
    /// <param name="storeId">Идентификатор магазина.</param>
    /// <param name="productsAndQuantities">Список товаров и их количеств.</param>
    /// <returns>Общее время приготовления.</returns>
    TimeSpan GetCookingTime(Guid storeId, List<ProductInventoryWithName> productsAndQuantities);
}