using OrderService.Domain.Models.Code;

namespace OrderService.Domain.Models.Order;

public class CanceledOrder : BaseOrder
{
    private CanceledOrder(Guid id, Guid clientId, Guid courierId, Guid storeId,
        List<BasketItem> basket, int price, string comment, TimeSpan cookingTime,
        TimeSpan deliveryTime, DateTime orderDate, DateTime? cookingDate, DateTime? deliveryDate,
        string cheque, StatusCode lastStatus, string reasonOfCanceled, DateTime CanceledDate)
        : base(id, clientId, courierId, storeId, basket, price, comment,
            cookingTime, deliveryTime, orderDate, cookingDate, deliveryDate, cheque)
    {
        LastStatus = lastStatus;
        ReasonOfCanceled = reasonOfCanceled;
    }

    public StatusCode LastStatus { get; }
    public string ReasonOfCanceled { get; } = string.Empty;
    
    public DateTime CanceledDate { get; } = DateTime.UtcNow;
    
    public static (CanceledOrder Order, string Error) Create(Guid id, Guid clientId,
        Guid courierId, Guid storeId, List<BasketItem> basket, int price, string comment, TimeSpan cookingTime,
        TimeSpan deliveryTime, DateTime orderDate, DateTime? cookingDate, DateTime? deliveryDate,
        string cheque, StatusCode lastStatus, string reasonOfCanceled, DateTime CanceledDate)
    {
        string errorString = Check(id, clientId, courierId, storeId, basket,
            price, comment, cookingTime, deliveryTime, cheque);

        if (string.IsNullOrEmpty(reasonOfCanceled) || reasonOfCanceled.Length > MaxCommentLength)
            errorString = "Error in reason of canceled, the value is empty or exceeds the maximum value";
       
        var order = new CanceledOrder(id, clientId, courierId, storeId, basket,
            price, comment, cookingTime, deliveryTime, orderDate, cookingDate,
            deliveryDate, cheque, lastStatus, reasonOfCanceled, CanceledDate);

        return (order, errorString);
    }
}