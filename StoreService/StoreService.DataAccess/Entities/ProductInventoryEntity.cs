namespace StoreService.DataAccess.Entities;

public class ProductInventoryEntity
{
    public Guid StoreId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}