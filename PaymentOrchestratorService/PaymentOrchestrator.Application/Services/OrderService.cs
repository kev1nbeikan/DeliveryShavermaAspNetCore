using BarsGroupProjectN1.Core.Contracts.Orders;
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

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
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

        return (orderCreateRequest, error);
    }
}