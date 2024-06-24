using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Common;
using Handler.Core.Extensions;
using Handler.Core.HanlderService;
using Handler.Core.Payment;

namespace HandlerService.Application.Services;

public class TemporyOrderService : IHandlerOrderService
{
    private readonly IHandlerRepository _handlerRepository;

    public TemporyOrderService(IHandlerRepository handlerRepository)
    {
        _handlerRepository = handlerRepository;
    }

    public (TemporyOrder? order, string? error) Save(Guid newGuid, Guid userId, Product[] products,
        List<ProductQuantity> productIdsAndQuantity,
        int price,
        string address,
        string comment)
    {
        var (order, error) = TemporyOrder.Create(
            newGuid,
            products,
            productIdsAndQuantity,
            price,
            comment,
            address,
            userId,
            userId
        );

        if (!string.IsNullOrEmpty(error)) return (null, error);

        error = _handlerRepository.Save(order);

        return (order, error);
    }


    public TemporyOrder? Get(Guid orderId)
    {
        return _handlerRepository.Get(orderId);
    }
}