using Newtonsoft.Json.Linq;

namespace OrderService.Domain.Models;

public class LastOrder : OrderBase
{
    private LastOrder(Guid id, Guid clientId, Guid courierId, Guid storeId,
        JObject basket, int price, string comment, string clientAddress,
        string courierNumber, string clientNumber, TimeSpan cookingTime,
        TimeSpan deliveryTime, string cheque, DateTime orderDate,
        DateTime cookingDate, DateTime deliveryDate)
        : base(id, clientId, courierId, storeId, basket,
            price, comment, clientAddress, courierNumber, clientNumber, cookingTime,
            deliveryTime)
    {
        Cheque = cheque;
        OrderDate = orderDate;
        CookingDate = cookingDate;
        DeliveryDate = deliveryDate;
    }

    public string Cheque { get; } // пока не уверен как хранить чек
    public DateTime OrderDate { get; }
    public DateTime CookingDate { get; }
    public DateTime DeliveryDate { get; }


    public static (LastOrder Order, string Error) Create(
        Guid id, Guid clientId, Guid courierId, Guid storeId,
        JObject basket, int price, string comment,
        string clientAddress, string courierNumber, string clientNumber,
        TimeSpan cookingTime, TimeSpan deliveryTime, string cheque, DateTime orderDate,
        DateTime cookingDate, DateTime deliveryDate)
    {
        string errorString = Check(id, clientId, courierId, storeId, basket,
            price, comment, clientAddress, courierNumber, clientNumber, cookingTime,
            deliveryTime);

        if (string.IsNullOrEmpty(cheque))
            errorString = "Error in cheque, value is empty";

        var order = new LastOrder(id, clientId, courierId, storeId, basket,
            price, comment, clientAddress, courierNumber, clientNumber, cookingTime,
            deliveryTime, cheque, orderDate, cookingDate, deliveryDate);

        return (order, errorString);
    }
}