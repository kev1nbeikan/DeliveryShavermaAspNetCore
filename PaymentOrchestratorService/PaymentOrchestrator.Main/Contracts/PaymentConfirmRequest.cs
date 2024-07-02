using System.ComponentModel.DataAnnotations;

namespace HandlerService.Contracts;

public class PaymentConfirmRequest
{
    public string PaymentType { get; set; }
    public string? CardNumber { get; set; }

    public string? ExpiryDate { get; set; }

    public string? CVV { get; set; }
    

    public Guid OrderId { get; set; }
}