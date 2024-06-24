namespace Handler.Core.Payment;

public record ProductQuantity
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
}