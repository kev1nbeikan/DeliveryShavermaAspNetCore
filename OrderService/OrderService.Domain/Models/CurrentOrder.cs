using Newtonsoft.Json.Linq;

namespace OrderService.Domain.Models;

public class CurrentOrder : OrderBase
{
    private CurrentOrder(Guid id, Guid clientId, Guid courierId, Guid storeId,
        JObject basket, int price, string comment, string clientAddress,
        string courierNumber, string clientNumber, TimeSpan cookingTime,
        TimeSpan deliveryTime, StatusCode status)
        : base(id, clientId, courierId, storeId,
        basket, price, comment,
        clientAddress, courierNumber, clientNumber, cookingTime, deliveryTime)
    {
        Status = status;
    }

    public StatusCode Status { get; }

    public static (CurrentOrder Order, string Error) Create(
        Guid id, Guid clientId, Guid courierId, Guid storeId,
        JObject basket, int price, string comment,
        string clientAddress, string courierNumber, string clientNumber,
        TimeSpan cookingTime, TimeSpan deliveryTime, StatusCode status)
    {
        string errorString = Check(id, clientId, courierId, storeId, basket,
            price, comment, clientAddress, courierNumber, clientNumber, cookingTime,
            deliveryTime);

        var order = new CurrentOrder(id, clientId, courierId, storeId, basket, 
            price, comment, clientAddress, courierNumber, clientNumber, cookingTime,
            deliveryTime, status);

        return (order, errorString);
    }
}