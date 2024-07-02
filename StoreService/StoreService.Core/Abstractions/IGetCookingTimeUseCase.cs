using System;
using System.Collections.Generic;
using BarsGroupProjectN1.Core.Models.Payment;

namespace StoreService.Core.Abstractions;

public interface IGetCookingTimeUseCase
{
    TimeSpan GetCookingTime(Guid storeId, List<ProductsInventory> productsAndQuantities);
}