using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Extensions;
using Handler.Core.HanlderService;

namespace HandlerService.Application.Services;

public class HandlerOrderService : IHandlerOrderService
{
    private readonly IHandlerRepository _handlerRepository;

    public HandlerOrderService(IHandlerRepository handlerRepository)
    {
        _handlerRepository = handlerRepository;
    }

    public (TemporyOrder? order, string? error) Save(Guid newGuid, Guid userId, Product[] products,
        int price,
        string address,
        string comment)
    {
        var (order, error) = TemporyOrder.Create(
            newGuid,
            products,
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