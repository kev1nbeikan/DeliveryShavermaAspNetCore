using Newtonsoft.Json.Linq;

namespace OrderService.Domain.Models;

public class CanceledOrder : OrderBase
{
    private CanceledOrder(Guid id, Guid clientId, Guid courierId, Guid storeId,
        JObject basket, int price, string comment, string clientAddress,
        string courierNumber, string clientNumber, TimeSpan cookingTime,
        TimeSpan deliveryTime, DateTime orderDate, DateTime cookingDate,
        DateTime deliveryDate, string cheque, StatusCode lastStatus, String reasonOfCanceled)
        : base(id, clientId, courierId, storeId, basket,
            price, comment, clientAddress, courierNumber,
            clientNumber, cookingTime, deliveryTime, orderDate,
            cookingDate, deliveryDate, cheque)
    {
        LastStatus = lastStatus;
        ReasonOfCanceled = reasonOfCanceled;
    }
    public StatusCode LastStatus { get; }
    public string ReasonOfCanceled { get; } = string.Empty;

    public static (CanceledOrder Order, string Error) Create(Guid id, Guid clientId,
        Guid courierId, Guid storeId, JObject basket, int price, string comment,
        string clientAddress, string courierNumber, string clientNumber, TimeSpan cookingTime,
        TimeSpan deliveryTime, DateTime orderDate, DateTime cookingDate, DateTime deliveryDate,
        string cheque, StatusCode lastStatus, String reasonOfCanceled)
    {
        string errorString = Check(id, clientId, courierId, storeId, basket,
            price, comment, clientAddress, courierNumber, clientNumber, cookingTime,
            deliveryTime, cheque);
        
        if (string.IsNullOrEmpty(reasonOfCanceled) || reasonOfCanceled.Length > MaxCommentLength)
            errorString = "Error in reason of canceled, the value is empty or exceeds the maximum value";

        var order = new CanceledOrder(id, clientId, courierId, storeId, basket,
            price, comment, clientAddress, courierNumber, clientNumber, cookingTime,
            deliveryTime, orderDate, cookingDate, deliveryDate, cheque, lastStatus, reasonOfCanceled);

        return (order, errorString);
    }
}