using Newtonsoft.Json.Linq;

namespace OrderService.Domain.Models;

public abstract class OrderBase
{
    
    public const int MaxNumberLength = 10;
    public const int MaxAddressLength = 250;
    public const int MaxCommentLength = 250;
    protected OrderBase(Guid id, Guid clientId, Guid courierId, Guid storeId,
        JObject basket, int price, string comment,
        string clientAddress, string courierNumber, string clientNumber,
        TimeSpan cookingTime, TimeSpan deliveryTime)
    {
        Id = id;
        ClientId = clientId;
        CourierId = courierId;
        StoreId = storeId;
        Basket = basket;
        Price = price;
        Comment = comment;
        ClientAddress = clientAddress;
        CourierNumber = courierNumber;
        ClientNumber = clientNumber;
        CookingTime = cookingTime;
        DeliveryTime = deliveryTime;
    }

    public Guid Id { get; }
    public Guid ClientId { get; }
    public Guid CourierId { get; }
    public Guid StoreId { get; }
    public JObject Basket { get; } = [];
    public int Price { get; }
    public string Comment { get; } = string.Empty;
    public string ClientAddress { get; } = string.Empty;
    public string CourierNumber { get; } = string.Empty;
    public string ClientNumber { get; } = string.Empty;
    public TimeSpan CookingTime { get; } = TimeSpan.Zero;
    public TimeSpan DeliveryTime { get; } = TimeSpan.Zero;


    public static String Check(
        Guid id, Guid clientId, Guid courierId, Guid storeId,
        JObject basket, int price, string comment,
        string clientAddress, string courierNumber, string clientNumber,
        TimeSpan cookingTime, TimeSpan deliveryTime)
    {
        string errorString = string.Empty;

        if (string.IsNullOrEmpty(clientAddress) || clientAddress.Length > MaxAddressLength)
            errorString = "Error in client address, the value is empty or exceeds the maximum value";

        if (string.IsNullOrEmpty(clientNumber) || clientNumber.Length > MaxNumberLength)
            errorString = "Error in client number, the value is empty or exceeds the maximum value";

        if (string.IsNullOrEmpty(courierNumber) || courierNumber.Length > MaxNumberLength)
            errorString = "Error in the courier number, the value is empty or exceeds the maximum value";

        if (basket.Count > 0)
            errorString = "Error in the basket, the value is empty";

        if (clientId == Guid.Empty)
            errorString = "Error in clientId, value is empty";

        if (courierId == Guid.Empty)
            errorString = "Error in courierId, value is empty";

        if (storeId == Guid.Empty)
            errorString = "Error in storeId, value is empty";

        if (price < 0)
            errorString = "Error in price, negative value for price";

        if (cookingTime < TimeSpan.Zero)
            errorString = "Error in cookingTime, negative value for price";

        if (deliveryTime < TimeSpan.Zero)
            errorString = "Error in deliveryTime, negative value for price";
        
        return errorString;
    }
}

