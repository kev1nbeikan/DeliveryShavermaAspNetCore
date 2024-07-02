using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Payment;
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
    private readonly ICurierService _courierService;
    private readonly IStoreRepository _storeRepository;

    public GetOrderLogisticUseCase(ICurierService courierService, IStoreRepository storeRepository)
    {
        _courierService = courierService;
        _storeRepository = storeRepository;
    }


    public async Task<(OrderLogistic? orderTimings, string? error)> Execute(PaymentOrder paymentOrder)
    {
        var result = new OrderLogistic();

        // (result.Delivery.Perfomer, result.Delivery.Time) =
        // await _curierService.GetCurier(temporyOrder.ClientAddress);
        // if (result.Delivery.Perfomer == null) return ExecuteErrorResult("Curier is not found");
        result.Delivering = new OrderTaskExecution<Curier>()
        {
            Executor = new Curier(Guid.NewGuid(), "798578997"),
            Time = TimeSpan.FromMinutes(15)
        };

        var productsAndAmount = paymentOrder.Bucket
            .Select(x => new ProductsInventory() { ProductId = x.product.Id, Quantity = x.amount }).ToList();

        (result.Cooking, var error) =
            await _storeRepository.GetCokingTime(paymentOrder.ClientAddress, productsAndAmount);

        return error.HasValue() ? 
            ExecuteErrorResult(error!) :
            (result, null);
    }


    private (OrderLogistic? orderTimings, string error) ExecuteErrorResult(string error)
    {
        return (null, error);
    }
}