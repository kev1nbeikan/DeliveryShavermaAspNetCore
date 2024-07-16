using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Exceptions;

namespace OrderService.Domain.Models;

/// <summary>
/// Модель отменного заказа.
/// </summary>
public class CanceledOrder : BaseOrder
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="CanceledOrder"/>.
    /// </summary>
    private CanceledOrder(Guid id, Guid clientId, Guid courierId, Guid storeId,
        List<BasketItem> basket, int price, string comment, TimeSpan cookingTime,
        TimeSpan deliveryTime, DateTime orderDate, DateTime? cookingDate, DateTime? deliveryDate,
        string cheque, StatusCode lastStatus, string reasonOfCanceled, DateTime canceledDate, RoleCode whoCanceled)
        : base(id, clientId, courierId, storeId, basket, price, comment,
            cookingTime, deliveryTime, orderDate, cookingDate, deliveryDate, cheque)
    {
        LastStatus = lastStatus;
        ReasonOfCanceled = reasonOfCanceled;
        CanceledDate = canceledDate;
        WhoCanceled = whoCanceled;
    }

    /// <summary> Последний статус заказа перед отменой. </summary>
    public StatusCode LastStatus { get; }

    /// <summary> Причина отмены заказа. </summary>
    public string ReasonOfCanceled { get; } = string.Empty;
    
    /// <summary> Дата и время отмены заказа. </summary>
    public DateTime CanceledDate { get; } = DateTime.UtcNow;
    
    /// <summary> Роль пользователя, отменившего заказ. </summary>
    public RoleCode WhoCanceled { get; }


    /// <summary>
    /// Создает и проверяет новый отмененный заказ.
    /// </summary>
    /// <returns>Новый отмененный заказ.</returns>
    /// <exception cref="FailToCreateOrderModel">Исключение выбрасывается, если данные заказа некорректны.</exception>
    public static CanceledOrder Create(Guid id, Guid clientId,
        Guid courierId, Guid storeId, List<BasketItem> basket, int price, string comment, TimeSpan cookingTime,
        TimeSpan deliveryTime, DateTime orderDate, DateTime? cookingDate, DateTime? deliveryDate,
        string cheque, StatusCode lastStatus, string reasonOfCanceled, DateTime canceledDate, RoleCode whoCanceled)
    {
        Check(id, clientId, courierId, storeId, basket,
            price, comment, cookingTime, deliveryTime, cheque);

        if (string.IsNullOrEmpty(reasonOfCanceled) || reasonOfCanceled.Length > MaxCommentLength)
            throw new FailToCreateOrderModel(
                "Ошибка в причине отмены, это поле не может быть пустым или превышает максимальное значение");
        
        if(!Enum.IsDefined(typeof(RoleCode), whoCanceled))
            throw new FailToCreateOrderModel(
                "Ошибка в роли отменяющего, такой роли не существует");
        
        if (!Enum.IsDefined(typeof(StatusCode), lastStatus))
            throw new FailToCreateOrderModel(
                "Ошибка в статусе заказа, такого статуса не существует");
       
        var order = new CanceledOrder(id, clientId, courierId, storeId, basket,
            price, comment, cookingTime, deliveryTime, orderDate, cookingDate,
            deliveryDate, cheque, lastStatus, reasonOfCanceled, canceledDate, whoCanceled);

        return order;
    }
}