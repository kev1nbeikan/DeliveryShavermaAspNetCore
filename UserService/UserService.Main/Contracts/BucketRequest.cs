namespace UserService.Main.Contracts;

public record BucketRequest
{
    public List<BucketItem> Bucket { get; set; }
}

