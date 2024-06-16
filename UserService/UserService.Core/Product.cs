namespace UserService.Core;

public class Product
{
    public const int MAX_TITLE_LENGTH = 32;

    public const int MAX_DESCRIPTION_LENGTH = 256;

    public const int MAX_COMPOSITION_LENGTH = 128;

    private Product(Guid id, string title, string description, string composition, int price, string imagePath)
    {
        Id = id;
        Title = title;
        Description = description;
        Composition = composition;
        Price = price;
        ImagePath = imagePath;
    }

    public Guid Id { get; }

    public string Title { get; }

    public string Description { get; }

    public string Composition { get; }

    public int Price { get; }

    public string ImagePath { get; }

    public static (Product product, string error) Create(
        Guid id,
        string title,
        string description,
        string composition,
        int price,
        string imagePath)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
        {
            error = "Title is empty or longer than 32 chars";
        }

        if (string.IsNullOrEmpty(description) || description.Length > MAX_DESCRIPTION_LENGTH)
        {
            error = "Title is empty or longer than 256 chars";
        }

        if (string.IsNullOrEmpty(composition) || composition.Length > MAX_COMPOSITION_LENGTH)
        {
            error = "Title is empty or longer than 128 chars";
        }

        if (price < 0)
        {
            error = "Price is negative";
        }

        if (string.IsNullOrEmpty(imagePath))
        {
            error = "Image path is empty";
        }

        var product = new Product(id, title, description, composition, price, imagePath);

        return (product, error);
    }
}