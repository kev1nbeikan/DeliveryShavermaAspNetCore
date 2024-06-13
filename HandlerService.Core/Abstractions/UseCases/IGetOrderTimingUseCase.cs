using HandlerService.Controllers;

namespace Handler.Core.Abstractions.UseCases;

public interface IGetOrderTimingUseCase
    
{
    Task<(TimeSpan cookingTime, TimeSpan deliveryTime, Curier curier, string?)> Invoke(
        HandlerServiceOrder handlerServiceOrder);
}