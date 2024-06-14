using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.UseCases;
using Handler.Core.Contracts;
using Handler.Core.HanlderService;
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


    public async Task<(OrderTimings? orderTimings, string? error)> Invoke(PaymentOrder paymentOrder)
    {
        var result = new OrderTimings();

        (result.DeliveryTime.Agent, result.DeliveryTime.Time) =
            await _curierService.GetCurier(paymentOrder.ClientAddress);
        if (result.DeliveryTime.Agent == null) return GetErrorResult("Curier is not found");

        (result.CookingTime.Time, var error) =
            await _storeService.GetCookingTime(paymentOrder.StoreId, paymentOrder.Basket);
        result.CookingTime.Agent = paymentOrder.StoreId;

        if (error.IsNotEmptyOrNull()) return GetErrorResult(error!);

        return (result, null);
    }


    private (OrderTimings? orderTimings, string error) GetErrorResult(string error)
    {
        return (null, error);
    }
}