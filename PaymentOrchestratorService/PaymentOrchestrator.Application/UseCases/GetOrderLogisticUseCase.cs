using BarsGroupProjectN1.Core.Models.Payment;
using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Abstractions.UseCases;
using Handler.Core.Contracts;
using Handler.Core.HanlderService;
using Handler.Core.Payment;
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


    public async Task<(OrderLogistic? orderTimings, string? error)> Execute(PaymentOrder paymentOrder)
    {
        var result = new OrderLogistic();

        // (result.Delivery.Perfomer, result.Delivery.Time) =
        // await _curierService.GetCurier(temporyOrder.ClientAddress);
        // if (result.Delivery.Perfomer == null) return ExecuteErrorResult("Curier is not found");

        var productsAndAmount = paymentOrder.Bucket.Select(x => new ProductsInventory() { ProductId = x.product.Id, Quantity = x.amount }).ToList();

        (result.Cooking, var error) =
            await _storeRepository.GetCokingTime(paymentOrder.ClientAddress, productsAndAmount);

        if (error.HasValue()) return ExecuteErrorResult(error!);

        return (result, null);
    }


    private (OrderLogistic? orderTimings, string error) ExecuteErrorResult(string error)
    {
        return (null, error);
    }
}