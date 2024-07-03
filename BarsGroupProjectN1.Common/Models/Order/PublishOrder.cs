using BarsGroupProjectN1.Core.Contracts.Orders;
using BarsGroupProjectN1.Core.Models.Order;

namespace BarsGroupProjectN1.Core.Models;

public class PublishOrder
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid CourierId { get; set; }
    public Guid StoreId { get; set; }
    public List<BasketItem> Basket { get; set; }
    public int Price { get; set; }
    public string Comment { get; set; }
    public TimeSpan CookingTime { get; set; }
    public TimeSpan DeliveryTime { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? CookingDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public string Cheque { get; set; }
    public StatusCode Status { get; init; }
    public string StoreAddress { get; init; } = string.Empty;
    public string ClientAddress { get; init; } = string.Empty;
    public string CourierNumber { get; init; } = string.Empty;
    public string ClientNumber { get; init; } = string.Empty;
}