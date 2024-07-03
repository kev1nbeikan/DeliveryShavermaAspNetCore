using BarsGroupProjectN1.Core.Contracts.Orders;
using BarsGroupProjectN1.Core.Exceptions;
using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Common;
using Handler.Core.Contracts;
using Handler.Core.Extensions;
using Handler.Core.HanlderService;
using HandlerService.Infustucture.Extensions;

namespace HandlerService.Application.Services;

public class OrderService : IOrderService
{
    private IOrderRepository _orderRepository;
    private IOrderPublisher _orderPublisher;

    public OrderService(IOrderRepository orderRepository, IOrderPublisher orderPublisher)
    {
        _orderRepository = orderRepository;
        _orderPublisher = orderPublisher;
    }


    public async Task<(OrderCreateRequest order, string? error)> Save(PaymentOrder order, OrderLogistic orderLogistic,
        string cheque,
        MyUser user)
    {
        var basketToRequest = order.Bucket.Select(x => new BasketItem()
            {
                ProductId = x.product.Id,
                Name = x.product.Title,
                Amount = x.amount,
                Price = x.product.Price
            })
            .ToList();

        // TODO adrress in store
        var orderCreateRequest = new OrderCreateRequest
        (
            Id: order.Id,
            ClientId: order.ClientId,
            CourierId: orderLogistic.Delivering.Executor!.Id,
            StoreId: orderLogistic.Cooking.Executor!.Id,
            Basket: basketToRequest,
            Price: order.Price,
            Comment: order.Comment,
            StoreAddress: "адресс",
            ClientAddress: order.ClientAddress,
            CourierNumber: orderLogistic.Delivering.Executor.PhoneNumber,
            ClientNumber: order.ClientNumber,
            CookingTime: orderLogistic.Cooking.Time,
            DeliveryTime: orderLogistic.Delivering.Time,
            Cheque: cheque
        );

        var error = await _orderRepository.Save(orderCreateRequest);

        if (string.IsNullOrEmpty(error))
            error = await PublishOrderKafka(orderCreateRequest);

        return (orderCreateRequest, error);
    }

    private async Task<string?> PublishOrderKafka(OrderCreateRequest order)
    {
        try
        {
            await _orderPublisher.PublishOrderCreate(order);
        }
        catch (RepositoryException e)
        {   
            return e.Message;
        }

        return null;
    }
}