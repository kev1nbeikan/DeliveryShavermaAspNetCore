namespace Handler.Core;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long Price { get; set; }
    public string Description { get; set; }
    public List<string> Recipe { get; set; }
    public Guid StoreId { get; set; }

    public Product(long id, string name, long price, string description, List<string> recipe, Guid storeId)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
        Recipe = recipe;
        StoreId = storeId;
    }


    public static (Product? product, string? error) Create(long id, string name, long price, string description,
        List<string> recipe, Guid storeId)
    {
        string error = null;
        

        return (new Product(id, name, price, description, recipe, storeId), error);
    }
}