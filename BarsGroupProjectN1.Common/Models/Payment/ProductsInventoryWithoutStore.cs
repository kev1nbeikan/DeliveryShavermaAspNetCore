namespace BarsGroupProjectN1.Core.Models.Payment;

public record ProductsInventoryWithoutStore
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}