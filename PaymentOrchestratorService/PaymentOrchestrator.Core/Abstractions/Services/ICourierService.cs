using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Courier;
using Handler.Core.Common;

namespace Handler.Core.Abstractions.Services;

public interface ICourierService
{
    /// <summary>
    /// Находит курьера для выполнения заказа.
    /// </summary>
    /// <param name="clientAddress">Адрес клиента</param>
    /// <param name="storeAddress">Адрес магазина</param>
    /// <returns>Курьер и ошибка, если есть</returns>
    Task<(OrderTaskExecution<Courier>? courier, string? error)> GetDeliveryExecution(string clientAddress, string storeAddress);
}