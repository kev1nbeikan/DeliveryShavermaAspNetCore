using UserService.Core;

namespace UserService.Main.Contracts;

public record BucketRequest
{
    public List<BucketItem> Bucket { get; set; }
}

public record BucketItem
{
    public string Id { get; set; }
    public string Title { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
}