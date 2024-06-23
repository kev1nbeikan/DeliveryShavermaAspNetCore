using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Services;
using Handler.Core.Abstractions.UseCases;
using Handler.Core.Contracts;
using Handler.Core.HanlderService;
using HandlerService.Controllers;
using HandlerService.Infustucture.Extensions;

namespace HandlerService.Application.UseCases;

public class GetOrderLogisticUseCase : IGetOrderLogisticUseCase
{
    private readonly IStoreService _storeService;
    private readonly ICurierService _curierService;

    public GetOrderLogisticUseCase(IStoreService storeService, ICurierService curierService)
    {
        _curierService = curierService;
        _storeService = storeService;
    }


    public async Task<(OrderLogistic? orderTimings, string? error)> Invoke(TemporyOrder temporyOrder)
    {
        return (new OrderLogistic()
        {
            Delivery = new OrderTaskInfo<Curier>()
            {
                Perfomer = null,
                Time = TimeSpan.Zero
            },
            Cooking = new OrderTaskInfo<Guid>()
            {
                Perfomer = Guid.NewGuid(),
                Time = TimeSpan.Zero
            }
        }, null);


        var result = new OrderLogistic();

        (result.Delivery.Perfomer, result.Delivery.Time) =
            await _curierService.GetCurier(temporyOrder.ClientAddress);
        if (result.Delivery.Perfomer == null) return ExecuteErrorResult("Curier is not found");

        (result.Cooking.Time, var error) =
            await _storeService.GetCookingTime(temporyOrder.StoreId, temporyOrder.Basket);
        result.Cooking.Perfomer = temporyOrder.StoreId;

        if (error.HasValue()) return ExecuteErrorResult(error);

        return (result, null);
    }


    private (OrderLogistic? orderTimings, string error) ExecuteErrorResult(string error)
    {
        return (null, error);
    }
}