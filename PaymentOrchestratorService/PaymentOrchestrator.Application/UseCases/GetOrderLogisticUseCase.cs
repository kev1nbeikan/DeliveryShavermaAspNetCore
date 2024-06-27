using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Abstractions.UseCases;
using Handler.Core.Contracts;
using Handler.Core.HanlderService;
using HandlerService.Infustucture.Extensions;

namespace HandlerService.Application.UseCases;

public class GetOrderLogisticUseCase : IGetOrderLogisticUseCase
{
    private readonly ICurierService _curierService;
    private readonly IStoreRepository _storeRepository;

    public GetOrderLogisticUseCase(ICurierService curierService, IStoreRepository storeRepository)
    {
        _curierService = curierService;
        _storeRepository = storeRepository;
    }


    public async Task<(OrderLogistic? orderTimings, string? error)> Execute(TemporyOrder temporyOrder)
    {
        var result = new OrderLogistic();

        // (result.Delivery.Perfomer, result.Delivery.Time) =
        // await _curierService.GetCurier(temporyOrder.ClientAddress);
        // if (result.Delivery.Perfomer == null) return ExecuteErrorResult("Curier is not found");

        (result.Cooking, var error) =
            await _storeRepository.GetCokingTime(temporyOrder.ClientAddress, temporyOrder.ProductsAndQuantities);

        if (error.HasValue()) return ExecuteErrorResult(error!);

        return (result, null);
    }


    private (OrderLogistic? orderTimings, string error) ExecuteErrorResult(string error)
    {
        return (null, error);
    }
}