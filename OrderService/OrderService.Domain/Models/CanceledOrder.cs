using Newtonsoft.Json.Linq;

namespace OrderService.Domain.Models;

public class CanceledOrder : OrderBase
{
    private CanceledOrder(Guid id, Guid clientId, Guid courierId, Guid storeId,
        JObject basket, int price, string comment,
        string clientAddress, string courierNumber, string clientNumber,
        TimeSpan cookingTime, TimeSpan deliveryTime, String cheque, DateTime orderDate, DateTime cookingDate,
        DateTime deliveryDate, StatusCode lastStatus, String reasonOfCanceled)
        : base(id, clientId, courierId, storeId,
            basket, price, comment, clientAddress, courierNumber, clientNumber, cookingTime, deliveryTime)
    {
        Cheque = cheque;
        OrderDate = orderDate;
        CookingDate = cookingDate;
        DeliveryDate = deliveryDate;
        LastStatus = lastStatus;
        ReasonOfCanceled = reasonOfCanceled;
    }

    public string Cheque { get; } = string.Empty; // пока не уверен как хранить чек
    public DateTime OrderDate { get; } = DateTime.UtcNow;
    public DateTime CookingDate { get; } = DateTime.UtcNow;
    public DateTime DeliveryDate { get; } = DateTime.UtcNow;

    public StatusCode LastStatus { get; }
    public string ReasonOfCanceled { get; } = string.Empty;

    public static (CanceledOrder Order, string Error) Create(Guid id, Guid clientId, Guid courierId, Guid storeId,
        JObject basket, int price, string comment,
        string clientAddress, string courierNumber, string clientNumber,
        TimeSpan cookingTime, TimeSpan deliveryTime, String cheque, DateTime orderDate, DateTime cookingDate,
        DateTime deliveryDate, StatusCode lastStatus, String reasonOfCanceled)
    {
        string errorString = Check(id, clientId, courierId, storeId, basket,
            price, comment, clientAddress, courierNumber, clientNumber, cookingTime,
            deliveryTime);

        if (string.IsNullOrEmpty(cheque))
            errorString = "Error in cheque, value is empty";
        
        if (string.IsNullOrEmpty(reasonOfCanceled) || reasonOfCanceled.Length > MaxCommentLength)
            errorString = "Error in reason of canceled, the value is empty or exceeds the maximum value";

        var order = new CanceledOrder(id, clientId, courierId, storeId,
            basket, price, comment, clientAddress, courierNumber, clientNumber,
            cookingTime, deliveryTime, cheque, orderDate, cookingDate,
            deliveryDate, lastStatus, reasonOfCanceled);

        return (order, errorString);
    }
}