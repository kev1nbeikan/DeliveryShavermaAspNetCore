using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Models;

namespace OrderService.Application.Extensions;

public static class PublishedOrderExtensions
{
    public static PublishOrder ToPublishedOrder(this CurrentOrder order)
    {
        return new PublishOrder
        {
            Id = order.Id,
            ClientId = order.ClientId,
            CourierId = order.CourierId,
            StoreId = order.StoreId,
            Basket = order.Basket,
            Price = order.Price,
            Comment = order.Comment,
            CookingTime = order.CookingTime,
            DeliveryTime = order.DeliveryTime,
            OrderDate = order.OrderDate,
            CookingDate = order.CookingDate,
            DeliveryDate = order.DeliveryDate,
            Cheque = order.Cheque,
            Status = order.Status,
            StoreAddress = order.StoreAddress,
            ClientAddress = order.ClientAddress,
            CourierNumber = order.CourierNumber,
            ClientNumber = order.ClientNumber
        };
    }
}