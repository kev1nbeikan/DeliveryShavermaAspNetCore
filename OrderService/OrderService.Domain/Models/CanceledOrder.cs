using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Exceptions;

namespace OrderService.Domain.Models;

public class CanceledOrder : BaseOrder
{
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

    public StatusCode LastStatus { get; }
    public string ReasonOfCanceled { get; } = string.Empty;
    
    public DateTime CanceledDate { get; } = DateTime.UtcNow;
    
    public RoleCode WhoCanceled { get; }

    
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
       
        var order = new CanceledOrder(id, clientId, courierId, storeId, basket,
            price, comment, cookingTime, deliveryTime, orderDate, cookingDate,
            deliveryDate, cheque, lastStatus, reasonOfCanceled, canceledDate, whoCanceled);

        return order;
    }
}