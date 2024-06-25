using System;
using System.Collections.Generic;

namespace StoreService.Core.Abstractions;

public interface IGetCookingTimeUseCase
{
    TimeSpan GetCookingTime(Guid storeId, List<ProductInventory> productsAndQuantities);
}