using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Extensions;
using HandlerService.Controllers;
using HandlerService.Infustucture.Extensions;

namespace HandlerService.Application.Services;

public class OrderService : IOrderService
{
    private IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<string?> Save(Order order)
    {
        return await _orderRepository.Save(order);
    }

    public async Task<(Order order, string? error)> CreateOrder(Guid handlerServiceOrderId,
        Product[] orderBucket, int price,
        string comment, string cheque, string clientAddress, Curier curier, MyUser user, Guid storeId,
        TimeSpan cookingTime, TimeSpan deliveryTime)
    {
        var (order, error) = Order.CreateAndSave(
            handlerServiceOrderId,
            StatusCode.Cooking,
            orderBucket.ToOrderBucket(),
            price,
            comment,
            cheque!,
            clientAddress,
            curier.PhoneNumber,
            user.PhoneNumber,
            user.UserId,
            curier.Id,
            storeId,
            cookingTime,
            deliveryTime,
            default,
            default,
            default);

        if (error.IsNotEmptyOrNull()) error = await Save(order);
        return (order, error);
    }
}