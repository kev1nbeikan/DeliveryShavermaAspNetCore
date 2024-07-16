namespace BarsGroupProjectN1.Core.Models.Payment;

public record ProductInventoryWithName : ProductsInventory
{
    public string? Name { get; set; }
}