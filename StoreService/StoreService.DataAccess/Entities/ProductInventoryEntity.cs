using System.ComponentModel.DataAnnotations;

namespace StoreService.DataAccess.Entities;

public class ProductInventoryEntity
{
    [Key]
    public Guid ProductId { get; set; }
    public Guid StoreId { get; set; }
    public int Quantity { get; set; }
}