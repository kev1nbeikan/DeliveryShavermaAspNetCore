namespace OrderService.DataAccess.Entities;

/// <summary>
/// Класс для сущностей отмененных заказов.
/// </summary>
public class CanceledOrderEntity : BaseOrderEntity
{
    public int LastStatus { get; set; }
    public string ReasonOfCanceled { get; set; } = string.Empty;
    public DateTime CanceledDate { get; set; } = DateTime.UtcNow;
    public int WhoCanceled { get; set; }
}