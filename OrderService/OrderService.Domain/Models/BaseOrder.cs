using Newtonsoft.Json.Linq;

namespace OrderService.Domain.Models;

public abstract class BaseOrder
{
    
    public const int MaxNumberLength = 10;
    public const int MaxAddressLength = 250;
    public const int MaxCommentLength = 250;
    public const int MaxChequeLength = 500;

    protected BaseOrder(Guid id, Guid clientId, Guid courierId, Guid storeId,
        JObject basket, int price, string comment, TimeSpan cookingTime, TimeSpan deliveryTime,
        DateTime orderDate, DateTime? cookingDate, DateTime? deliveryDate, string cheque)
    {
        Id = id;
        ClientId = clientId;
        CourierId = courierId;
        StoreId = storeId;
        Basket = basket;
        Price = price;
        Comment = comment;
        CookingTime = cookingTime;
        DeliveryTime = deliveryTime;
        OrderDate = orderDate;
        CookingDate = cookingDate;
        DeliveryDate = deliveryDate;
        Cheque = cheque;
    }

    public Guid Id { get; }
    public Guid ClientId { get; }
    public Guid CourierId { get; }
    public Guid StoreId { get; }
    public JObject Basket { get; } = [];
    public int Price { get; }
    public string Comment { get; } = string.Empty;
    public TimeSpan CookingTime { get; } = TimeSpan.Zero;
    public TimeSpan DeliveryTime { get; } = TimeSpan.Zero;
    public DateTime OrderDate { get; } = DateTime.UtcNow;
    public DateTime? CookingDate { get; } = DateTime.UtcNow;
    public DateTime? DeliveryDate { get; } = DateTime.UtcNow;
    public string Cheque { get; } = String.Empty; // пока не уверен как хранить чек

    protected static String Check(
        Guid id, Guid clientId, Guid courierId, Guid storeId,
        JObject basket, int price, string comment, TimeSpan cookingTime,
        TimeSpan deliveryTime, string cheque)
    {
        string errorString = string.Empty;

        if (comment.Length > MaxCommentLength)
            errorString = "Error in the comment, the value is exceeds the maximum value";

        if (basket.Count > 0)
            errorString = "Error in the basket, the value is empty";

        if (id == Guid.Empty)
            errorString = "Error in id, value is empty";
        
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
        
        if (string.IsNullOrEmpty(cheque) || cheque.Length > MaxChequeLength)
            errorString = "Error in the cheque, the value is empty or exceeds the maximum value";

        return errorString;
    }
}

