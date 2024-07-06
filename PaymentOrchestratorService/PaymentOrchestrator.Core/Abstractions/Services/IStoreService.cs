using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Payment;
using BarsGroupProjectN1.Core.Models.Store;
using Handler.Core.Payment;

namespace Handler.Core.Abstractions.Services;

public interface IStoreService
{
    /// <summary>
    /// Получить информацию о готовке продуктов для указанного адреса и корзины продуктов.
    /// </summary>
    /// <param name="clientAddress">Адрес клиент, чтобы найти магазин около этого адреса.</param>
    /// <param name="basket">Корзина продуктов, для которых нужно получить информацию о готовке.</param>
    /// <returns>Кортеж, содержащий информацию о выполнении задачи готовки и возможную ошибку.</returns>
    Task<(OrderTaskExecution<Store>? cookingExecution, string? error)> GetCookingExecution(string clientAddress,
        List<ProductsInventory> basket);
}