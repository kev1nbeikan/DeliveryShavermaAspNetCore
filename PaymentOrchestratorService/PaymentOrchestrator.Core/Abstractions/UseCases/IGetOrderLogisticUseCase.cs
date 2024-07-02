using Handler.Core.Contracts;
using Handler.Core.HanlderService;

namespace Handler.Core.Abstractions.UseCases;

public interface IGetOrderLogisticUseCase
{
    Task<(OrderLogistic? orderTimings, string? error)> Execute(PaymentOrder paymentOrder);
}