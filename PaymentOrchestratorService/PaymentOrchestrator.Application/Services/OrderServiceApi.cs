using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Exceptions;
using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Store;
using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Common;
using Handler.Core.Contracts;
using Handler.Core.Extensions;
using Handler.Core.HanlderService;
using HandlerService.Infustucture.Extensions;
using UserService.Core;
using BasketItem = BarsGroupProjectN1.Core.Models.Order.BasketItem;

namespace HandlerService.Application.Services;

public class OrderServiceApi : IOrderService
{
    private IOrderRepository _orderRepository;


    public OrderServiceApi(IOrderRepository orderRepository)
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

        var orderCreateRequest = new OrderCreateRequest
        (
            Id: order.Id,
            ClientId: order.ClientId,
            CourierId: orderLogistic.Delivering.Executor!.Id,
            StoreId: orderLogistic.Cooking.Executor!.Id,
            Basket: basketToRequest,
            Price: order.Price,
            Comment: order.Comment,
            StoreAddress: orderLogistic.Cooking.Executor.Address,
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