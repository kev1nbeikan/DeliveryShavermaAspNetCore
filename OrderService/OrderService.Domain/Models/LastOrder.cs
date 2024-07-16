using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Common;

namespace OrderService.Domain.Models;

/// <summary>
/// Модель заказа для истории.
/// </summary>
public class LastOrder : BaseOrder
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="LastOrder"/>.
    /// </summary>
    private LastOrder(Guid id, Guid clientId, Guid courierId, Guid storeId,
        List<BasketItem> basket, int price, string comment, TimeSpan cookingTime,
        TimeSpan deliveryTime, DateTime orderDate, DateTime? cookingDate,
        DateTime? deliveryDate, string cheque)
        : base(id, clientId, courierId, storeId, basket, price, comment,
            cookingTime, deliveryTime, orderDate, cookingDate, deliveryDate, cheque)
    {
    }

    /// <summary>
    /// Создает новый заказ для истории. 
    /// </summary>
    /// <returns>Новый заказ для истории.</returns>
    public static LastOrder Create(Guid id, Guid clientId,
        Guid courierId, Guid storeId, List<BasketItem> basket, int price, string comment,
        TimeSpan cookingTime, TimeSpan deliveryTime, DateTime orderDate,
        DateTime? cookingDate, DateTime? deliveryDate, string cheque)
    {
        Check(id, clientId, courierId, storeId, basket,
            price, comment, cookingTime, deliveryTime, cheque);

        var order = new LastOrder(id, clientId, courierId, storeId, basket,
            price, comment, cookingTime, deliveryTime, orderDate,
            cookingDate, deliveryDate, cheque);

        return order;
    }
}