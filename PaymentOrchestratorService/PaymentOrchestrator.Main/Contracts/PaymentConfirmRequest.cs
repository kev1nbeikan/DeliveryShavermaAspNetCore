namespace HandlerService.Contracts;

public record PaymentConfirmRequest
{
    public string PaymentType { get; init; }
    public string CardNumber { get; init; }
    public string ExpiryDate { get; init; }
    public string CVV { get; init; }
    public string Comment { get; init; }
    public string Address { get; init; }
    public Guid OrderId { get; init; }
}