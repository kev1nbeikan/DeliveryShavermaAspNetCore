using Newtonsoft.Json.Linq;
using OrderService.Domain.Models.Code;

namespace OrderService.Domain.Models;

public class CurrentOrder : BaseOrder
{
    private CurrentOrder(Guid id, Guid clientId, Guid courierId, Guid storeId,
        JObject basket, int price, string comment, string storeAddress,
        string clientAddress, string courierNumber, string clientNumber,
        TimeSpan cookingTime, TimeSpan deliveryTime, DateTime orderDate,
        DateTime cookingDate, DateTime deliveryDate, string cheque, StatusCode status)
        : base(id, clientId, courierId, storeId, basket, price, comment,
            cookingTime, deliveryTime, orderDate, cookingDate, deliveryDate, cheque)
    {
        Status = status;
        StoreAddress = storeAddress;
        ClientAddress = clientAddress;
        CourierNumber = courierNumber;
        ClientNumber = clientNumber;
    }

    public StatusCode Status { get; }
    public string StoreAddress { get; } = string.Empty;
    public string ClientAddress { get; } = string.Empty;
    public string CourierNumber { get; } = string.Empty;
    public string ClientNumber { get; } = string.Empty;

    public static (CurrentOrder Order, string Error) Create(Guid id, Guid clientId,
        Guid courierId, Guid storeId, JObject basket, int price, string comment,
        string storeAddress, string clientAddress, string courierNumber, string clientNumber,
        TimeSpan cookingTime, TimeSpan deliveryTime, DateTime orderDate,
        DateTime cookingDate, DateTime deliveryDate, string cheque, StatusCode status)
    {
        string errorString = Check(id, clientId, courierId, storeId, basket,
            price, comment, cookingTime,
            deliveryTime, cheque);
        
        if (string.IsNullOrEmpty(clientAddress) || clientAddress.Length > MaxAddressLength)
            errorString = "Error in client address, the value is empty or exceeds the maximum value";

        if (string.IsNullOrEmpty(clientNumber) || clientNumber.Length > MaxNumberLength)
            errorString = "Error in client number, the value is empty or exceeds the maximum value";

        if (string.IsNullOrEmpty(courierNumber) || courierNumber.Length > MaxNumberLength)
            errorString = "Error in the courier number, the value is empty or exceeds the maximum value";
        
        if (string.IsNullOrEmpty(storeAddress) || storeAddress.Length > MaxNumberLength)
            errorString = "Error in the store address, the value is empty or exceeds the maximum value";

        var order = new CurrentOrder(id, clientId, courierId, storeId, basket,
            price, comment, storeAddress, clientAddress, courierNumber, clientNumber, cookingTime,
            deliveryTime, orderDate, cookingDate, deliveryDate, cheque, status);

        return (order, errorString);
    }
}