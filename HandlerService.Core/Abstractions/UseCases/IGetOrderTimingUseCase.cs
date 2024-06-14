using HandlerService.Application.Contracts;

namespace Handler.Core.Abstractions.UseCases;

public interface IGetOrderTimingUseCase
{
    Task<(OrderTimings? orderTimings, string? error)> Invoke(HandlerServiceOrder handlerServiceOrder);
}