namespace BarsGroupProjectN1.Core.Models.Payment;

public record ProductsInventory
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}