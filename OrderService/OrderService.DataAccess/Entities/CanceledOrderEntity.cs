namespace OrderService.DataAccess.Entities;
    public class CanceledOrderEntity
    {
    public Guid Id { get; set; }


    public Guid ClientId { get; set; }

    public Guid CourierId { get; set; }

    public Guid StoreId { get; set; }


    public string Basket { get; set; } = string.Empty;


    public int Price { get; set; }

    public int Grade { get; set; }

    
    public int LastStatus { get; set; }

    public string ReasonOfCanceled { get; set; } = string.Empty;


    public string Comment { get; set; } = string.Empty;

    public string Cheque { get; } = string.Empty; // пока не уверен как хранить чек

    public string CourierNumber { get; set; } = string.Empty;

    public string ClientNumber { get; set; } = string.Empty;

    public string ClientAddress { get; set; } = string.Empty;


    public TimeSpan CookingTime { get; set; } = TimeSpan.Zero;

    public TimeSpan DeliveryTime { get; set; } = TimeSpan.Zero;


    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public DateTime CookingDate { get; set; } = DateTime.UtcNow;

    public DateTime DeliveryDate { get; set; } = DateTime.UtcNow;
}

