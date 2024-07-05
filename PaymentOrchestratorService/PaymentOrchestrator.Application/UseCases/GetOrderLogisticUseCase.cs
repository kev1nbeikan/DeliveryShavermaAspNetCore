using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Courier;
using BarsGroupProjectN1.Core.Models.Payment;
using BarsGroupProjectN1.Core.Models.Store;
using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Abstractions.UseCases;
using Handler.Core.Common;
using Handler.Core.Contracts;
using Handler.Core.HanlderService;
using Handler.Core.Payment;
using HandlerService.Infustucture.Extensions;

namespace HandlerService.Application.UseCases;

public class GetOrderLogisticUseCase : IGetOrderLogisticUseCase
{
    private readonly IStoreRepository _storeRepository;
    private readonly ICurierRepository _courierRepository;

    public GetOrderLogisticUseCase(IStoreRepository storeRepository, ICurierRepository courierRepository)
    {
        _storeRepository = storeRepository;
        _courierRepository = courierRepository;
    }


    public async Task<(OrderLogistic? orderLogistic, string? error)> Execute(PaymentOrder paymentOrder)
    {
        var result = new OrderLogistic();

        var findStoreResult = await FindStore(paymentOrder);
        if (findStoreResult.error.HasValue()) return ExecuteErrorResult(findStoreResult.error!);

        var findCourierResult = await FindCourier(paymentOrder.ClientAddress, result.Cooking.Executor.Address);
        if (findCourierResult.error.HasValue()) return ExecuteErrorResult(findCourierResult.error!);

        result.Delivering = findCourierResult.deliveryExecution!;
        result.Cooking = findStoreResult.cookingExecution!;

        return (result, null);
    }


    private (OrderLogistic? orderTimings, string error) ExecuteErrorResult(string error)
    {
        return (null, error);
    }

    private async Task<(OrderTaskExecution<Store>? cookingExecution, string? error)> FindStore(
        PaymentOrder paymentOrder)
    {
        var productsAndAmount = paymentOrder.Bucket
            .Select(x =>
                new ProductsInventory() { ProductId = x.product.Id, Quantity = x.amount }
            ).ToList();

        return await _storeRepository.GetCokingTime(paymentOrder.ClientAddress, productsAndAmount);
    }

    private async Task<(OrderTaskExecution<Courier>? deliveryExecution, string? error)> FindCourier(
        string clientAddress, string storeAddress)
    {
        return await _courierRepository.FindCourier(clientAddress, storeAddress);
    }
}