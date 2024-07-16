namespace OrderService.DataAccess.Entities;

/// <summary>
/// Базовый класс для сущностей заказов.
/// </summary>
public abstract class BaseOrderEntity
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid CourierId { get; set; }
    public Guid StoreId { get; set; }
    public string Basket { get; set; } = string.Empty;
    public int Price { get; set; }
    public string Comment { get; set; } = string.Empty;
    public TimeSpan CookingTime { get; set; } = TimeSpan.Zero;
    public TimeSpan DeliveryTime { get; set; } = TimeSpan.Zero;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public DateTime? CookingDate { get; set; } = DateTime.UtcNow;
    public DateTime? DeliveryDate { get; set; } = DateTime.UtcNow;
    public string Cheque { get; set; } = String.Empty;
}