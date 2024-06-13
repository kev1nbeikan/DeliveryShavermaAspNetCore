using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.UseCases;
using HandlerService.Controllers;
using HandlerService.Infustucture.Extensions;

namespace HandlerService.Application.UseCases;

public class GetOrderTimingUseCase : IGetOrderTimingUseCase
{
    private readonly IStoreService _storeService;
    private readonly ICurierService _curierService;

    public GetOrderTimingUseCase(IStoreService storeService, ICurierService curierService)
    {
        _curierService = curierService;
        _storeService = storeService;
    }


    public async Task<(TimeSpan cookingTime, TimeSpan deliveryTime, Curier curier, string?)> Invoke(
        HandlerServiceOrder handlerServiceOrder)
    {
        var (curier, deliveryTime) = await _curierService.GetCurier(handlerServiceOrder.ClientAddress);
        if (curier == null) return GetErrorResult("Curier is not found");

        var (cookingTime, error) =
            await _storeService.GetCookingTime(handlerServiceOrder.StoreId, handlerServiceOrder.Basket);
        if (error.IsNotEmptyOrNull()) return GetErrorResult(error);

        return (cookingTime, deliveryTime, curier, null);
    }


    private (TimeSpan cookingTime, TimeSpan deliveryTime, Curier curier, string?) GetErrorResult(string error)
    {
        return (TimeSpan.Zero, TimeSpan.Zero, null, error)!;
    }
}