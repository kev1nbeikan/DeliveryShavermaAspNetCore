using Handler.Core.Contracts;
using Handler.Core.HanlderService;

namespace Handler.Core.Abstractions.UseCases;

public interface IGetOrderTimingUseCase
{
    Task<(OrderTimings? orderTimings, string? error)> Invoke(PaymentOrder paymentOrder);
}