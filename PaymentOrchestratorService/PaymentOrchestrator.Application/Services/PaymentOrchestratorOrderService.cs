using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Common;
using Handler.Core.Extensions;
using Handler.Core.HanlderService;
using Handler.Core.Payment;

namespace HandlerService.Application.Services;

public class PaymentOrchestratorOrderService : IHandlerOrderService
{
    private readonly IHandlerRepository _handlerRepository;

    public PaymentOrchestratorOrderService(IHandlerRepository handlerRepository)
    {
        _handlerRepository = handlerRepository;
    }

    public (PaymentOrder? order, string? error) Save(Guid newGuid, Guid userId, Product[] products,
        int price,
        string address,
        string comment, List<(Product product, int amount, int price)> productAndQuantity, string clientNumber)
    {
        var (order, error) = PaymentOrder.Create(
            newGuid,
            products,
            productAndQuantity,
            price,
            comment,
            address,
            userId,
            clientNumber
        );

        if (!string.IsNullOrEmpty(error)) return (null, error);

        error = _handlerRepository.Save(order);

        return (order, error);
    }


    public PaymentOrder? Get(Guid orderId)
    {
        return _handlerRepository.Get(orderId);
    }
}