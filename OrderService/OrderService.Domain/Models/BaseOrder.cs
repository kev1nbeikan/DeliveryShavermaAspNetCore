using System.Text.RegularExpressions;
using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Common;
using OrderService.Domain.Exceptions;

namespace OrderService.Domain.Models;

public abstract class BaseOrder
{
    public const int MaxNumberLength = 10;
    public const int MaxAddressLength = 250;
    public const int MaxCommentLength = 500;
    public const int MaxChequeLength = 500;
    
    public static readonly Regex RegexForNumber = new Regex(@"^[+0-9\s\-()]{6,15}$(?=.*[0-9]){11}");

    protected BaseOrder(Guid id, Guid clientId, Guid courierId, Guid storeId,
        List<BasketItem> basket, int price, string comment, TimeSpan cookingTime, TimeSpan deliveryTime,
        DateTime orderDate, DateTime? cookingDate, DateTime? deliveryDate, string cheque)
    {
        Id = id;
        ClientId = clientId;
        CourierId = courierId;
        StoreId = storeId;
        Basket = basket;
        Price = price;
        Comment = comment;
        CookingTime = cookingTime;
        DeliveryTime = deliveryTime;
        OrderDate = orderDate;
        CookingDate = cookingDate;
        DeliveryDate = deliveryDate;
        Cheque = cheque;
    }

    public Guid Id { get; }
    public Guid ClientId { get; }
    public Guid CourierId { get; }
    public Guid StoreId { get; }
    public List<BasketItem> Basket { get; }
    public int Price { get; }
    public string Comment { get; } = string.Empty;
    public TimeSpan CookingTime { get; } = TimeSpan.Zero;
    public TimeSpan DeliveryTime { get; } = TimeSpan.Zero;
    public DateTime OrderDate { get; } = DateTime.UtcNow;
    public DateTime? CookingDate { get; } = DateTime.UtcNow;
    public DateTime? DeliveryDate { get; } = DateTime.UtcNow;
    public string Cheque { get; } = String.Empty; // пока не уверен как хранить чек

    protected static void Check(
        Guid id, Guid clientId, Guid courierId, Guid storeId,
        List<BasketItem> basket, int price, string comment, TimeSpan cookingTime,
        TimeSpan deliveryTime, string cheque)
    {
        if (comment.Length > MaxCommentLength)
            throw new FailToCreateOrderModel(
                "Ошибка в комментарии заказа, поле отсутствует или превышает максимальное значение");
        if (basket.Count == 0)
            throw new FailToCreateOrderModel(
                "Ошибка в корзине заказа, поле отсутствует или пустое");

        if (id == Guid.Empty)
            throw new FailToCreateOrderModel(
                "Ошибка в id заказа, пустое значение");

        if (clientId == Guid.Empty)
            throw new FailToCreateOrderModel(
                "Ошибка в id клиента, пустое значение");

        if (courierId == Guid.Empty)
            throw new FailToCreateOrderModel(
                "Ошибка в id курьера, пустое значение");
        
        if (storeId == Guid.Empty)
            throw new FailToCreateOrderModel(
                "Ошибка в id предприятия, пустое значение");
        
        if (price < 0)
            throw new FailToCreateOrderModel(
                "Ошибка в цене заказа, отрицательное значение");
        
        if (cookingTime < TimeSpan.Zero)
            throw new FailToCreateOrderModel(
                "Ошибка в времени приготовления, отрицательное значение");

        if (deliveryTime < TimeSpan.Zero)
            throw new FailToCreateOrderModel(
                "Ошибка в времени доставки, отрицательное значение");

        if (string.IsNullOrEmpty(cheque) || cheque.Length > MaxChequeLength)
            throw new FailToCreateOrderModel(
                "Ошибка в чеке, поле отсутствует или превышает максимальное значение");
    }
}