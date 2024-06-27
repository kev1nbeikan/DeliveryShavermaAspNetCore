namespace StoreService.Core;

public class MenuProductInventory
{
    public MenuProductInventory(Guid id, string title, string description, string composition, int price)
    {
        Id = id;
        Title = title;
        Description = description;
        Composition = composition;
        Price = price;
    }

    public Guid Id { get; }

    public string Title { get; }

    public string Description { get; }

    public string Composition { get; }

    public int Price { get; }
}