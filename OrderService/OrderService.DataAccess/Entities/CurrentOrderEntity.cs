namespace OrderService.DataAccess.Entities;

public class CurrentOrderEntity : BaseOrderEntity
{
    public int Status { get; set; }
    public string StoreAddress { get; set; } = string.Empty;
    public string ClientAddress { get; set; } = string.Empty;
    public string CourierNumber { get; set; } = string.Empty;
    public string ClientNumber { get; set; } = string.Empty;
}