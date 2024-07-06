using Handler.Core.Contracts;
using Handler.Core.HanlderService;

namespace Handler.Core.Abstractions.UseCases;

/// <summary>
/// Юзкейс для получения информации о выполнении заказа, доставке и готовке.
/// </summary>
public interface IGetOrderLogisticUseCase
{
    /// <summary>
    /// Юзкейс для получения информации о выполнении заказа, доставке и готовке.
    /// </summary>
    Task<(OrderLogistic? orderLogistic, string? error)> Execute(PaymentOrder paymentOrder);
}