namespace OrderService.DataAccess.Entities;

public class CurrentOrderEntity
{
    public Guid Id { get; set; }


    public Guid ClientId { get; set; }

    public Guid CourierId { get; set; }

    public Guid StoreId { get; set; }


    public string Basket { get; set; } = string.Empty;


    public int Status { get; set; }

    public int Price { get; set; }


    public string Comment { get; set; } = string.Empty;

    public string CourierNumber { get; set; } = string.Empty;

    public string ClientNumber { get; set; } = string.Empty;

    public string ClientAddress { get; set; } = string.Empty;


    public TimeSpan CookingTime { get; set; } = TimeSpan.Zero;

    public TimeSpan DeliveryTime { get; set; } = TimeSpan.Zero;

}
