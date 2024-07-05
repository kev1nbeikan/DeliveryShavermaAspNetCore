using Handler.Core.Contracts;
using Handler.Core.HanlderService;

namespace Handler.Core.Abstractions.UseCases;

public interface IGetOrderLogisticUseCase
{
    Task<(OrderLogistic? orderLogistic, string? error)> Execute(PaymentOrder paymentOrder);
}