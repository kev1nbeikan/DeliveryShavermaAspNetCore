namespace Handler.Core.Payment;

public record BucketItem
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
}