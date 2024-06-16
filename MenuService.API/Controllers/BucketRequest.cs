namespace UserService.Main.Contracts;

public record BucketRequest
{
    public List<CartItem> Bucket { get; set; }
}

public record CartItem
{
    public string Id { get; set; }
    public int Quantity { get; set; }
}