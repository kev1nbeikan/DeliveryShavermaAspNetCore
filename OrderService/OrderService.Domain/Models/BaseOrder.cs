using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Common;
using OrderService.Domain.Exceptions;

namespace OrderService.Domain.Models;

/// <summary>
/// Базовый класс для модели заказа.
/// </summary>
public abstract class BaseOrder
{
    /// <summary> Максимальная длина номера. </summary>
    public const int MaxNumberLength = 15;
    
    /// <summary> Максимальная длина адреса. </summary>
    public const int MaxAddressLength = 250;

    /// <summary> Максимальная длина комментария. </summary>
    public const int MaxCommentLength = 500;

    /// <summary> Максимальная длина чека. </summary>
    public const int MaxChequeLength = 500;

    /// <summary> Регулярное выражение для проверки номера телефона. Типа +7(999)999-99-99 или 81234567890. </summary>
    protected static readonly Regex RegexForNumber = new Regex(@"^[\d\s+()-]{6,15}$");

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="BaseOrder"/>.
    /// </summary>
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

    /// <summary> Идентификатор заказа. </summary>
    public Guid Id { get; }

    /// <summary> Идентификатор клиента. </summary>
    public Guid ClientId { get; }

    /// <summary> Идентификатор курьера. </summary>
    public Guid CourierId { get; }

    /// <summary> Идентификатор магазина. </summary>
    public Guid StoreId { get; }

    /// <summary> Корзина заказа. </summary>
    public List<BasketItem> Basket { get; }

    /// <summary> Цена заказа. </summary>
    public int Price { get; }

    /// <summary> Комментарий к заказу. </summary>
    public string Comment { get; } = string.Empty;

    /// <summary> Время приготовления заказа. </summary>
    public TimeSpan CookingTime { get; } = TimeSpan.Zero;

    /// <summary> Время доставки заказа. </summary>
    public TimeSpan DeliveryTime { get; } = TimeSpan.Zero;

    /// <summary> Дата и время создания заказа. </summary>
    public DateTime OrderDate { get; } = DateTime.UtcNow;

    /// <summary> Дата и время начала приготовления заказа. </summary>
    public DateTime? CookingDate { get; } = DateTime.UtcNow;

    /// <summary> Дата и время доставки заказа. </summary>
    public DateTime? DeliveryDate { get; } = DateTime.UtcNow;

    /// <summary> Чек заказа. </summary>
    public string Cheque { get; } = String.Empty;

    /// <summary>
    /// Проверяет корректность данных заказа.
    /// </summary>
    /// <exception cref="FailToCreateOrderModel">Исключение выбрасывается, если данные заказа некорректны.</exception>
    protected static void Check(
        Guid id, Guid clientId, Guid courierId, Guid storeId,
        List<BasketItem> basket, int price, string comment, TimeSpan cookingTime,
        TimeSpan deliveryTime, string cheque)
    {
        if (comment.Length > MaxCommentLength)
            throw new FailToCreateOrderModel(
                "Ошибка в комментарии заказа, поле отсутствует или превышает максимальное значение");

        if (basket.Count == 0 || basket == null)
            throw new FailToCreateOrderModel(
                $"Ошибка в корзине заказа, поле отсутствует или пустое{JsonSerializer.Serialize(basket)}");

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