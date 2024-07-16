using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Models;

namespace OrderService.Application.Extensions;

/// <summary>
/// Класс расширений для преобразования объектов <see cref="CurrentOrder"/> в объекты <see cref="PublishOrder"/>.
/// </summary>
public static class PublishedOrderExtensions
{
    /// <summary>
    /// Преобразует объект <see cref="CurrentOrder"/> в объект <see cref="PublishOrder"/>.
    /// </summary>
    /// <param name="order">Объект <see cref="CurrentOrder"/>, который нужно преобразовать.</param>
    /// <returns>Объект <see cref="PublishOrder"/>, созданный на основе данных из <see cref="CurrentOrder"/>.</returns>
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