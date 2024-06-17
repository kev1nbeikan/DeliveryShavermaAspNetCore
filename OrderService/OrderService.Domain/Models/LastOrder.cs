using Newtonsoft.Json.Linq;

namespace OrderService.Domain.Models;

public class LastOrder : BaseOrder
{
    private LastOrder(Guid id, Guid clientId, Guid courierId, Guid storeId,
        JObject basket, int price, string comment, string clientAddress,
        string courierNumber, string clientNumber, TimeSpan cookingTime,
        TimeSpan deliveryTime, DateTime orderDate, DateTime cookingDate,
        DateTime deliveryDate, string cheque)
        : base(id, clientId, courierId, storeId, basket,
            price, comment, clientAddress, courierNumber,
            clientNumber, cookingTime, deliveryTime, orderDate,
            cookingDate, deliveryDate, cheque)
    {
    }
    
    public static (LastOrder Order, string Error) Create(Guid id, Guid clientId,
        Guid courierId, Guid storeId, JObject basket, int price, string comment,
        string clientAddress, string courierNumber, string clientNumber,
        TimeSpan cookingTime, TimeSpan deliveryTime, DateTime orderDate,
        DateTime cookingDate, DateTime deliveryDate, string cheque)
    {
        string errorString = Check(id, clientId, courierId, storeId, basket,
            price, comment, clientAddress, courierNumber, clientNumber, cookingTime,
            deliveryTime, cheque);

        var order = new LastOrder(id, clientId, courierId, storeId, basket,
            price, comment, clientAddress, courierNumber, clientNumber,
            cookingTime, deliveryTime, orderDate, cookingDate, deliveryDate, cheque);

        return (order, errorString);
    }
}