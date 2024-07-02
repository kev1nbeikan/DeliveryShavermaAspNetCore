namespace Handler.Core.Abstractions.Services;

public class Payment
{
    public PaymentType PaymentType { get; set; }
    public Card? Card { get; set; }
}

public class Card
{
    public string? CardNumber { get; init; }
    public string? ExpiryDate { get; init; }
    public string? CVV { get; init; }
}