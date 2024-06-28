namespace StoreService.Core;

public class MappedProduct
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Composition { get; set; }
    public int Price { get; set; }
    public string ImagePath { get; set; }
    public int Quantity { get; set; }
}