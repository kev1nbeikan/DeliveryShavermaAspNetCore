namespace Handler.Core.Payment;

public record ProductWithAmount
{
    public ProductWithAmount(Guid productId, int amount)
    {
        Id = productId;
        Quantity = amount;
    }

    public ProductWithAmount()
    {
    }

    public Guid Id { get; set; }
    public int Quantity { get; set; }
}