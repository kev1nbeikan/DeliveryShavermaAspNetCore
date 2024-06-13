namespace Menu.Core;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public List<string> Recipe { get; set; }
    public Guid StoreId { get; set; }
}