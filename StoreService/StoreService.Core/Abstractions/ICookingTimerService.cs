using System;
using System.Collections.Generic;

namespace StoreService.Core.Abstractions;

public interface ICookingTimerService
{
    TimeSpan GetCookingTime(Guid storeId, List<ProductInventory> products);
}