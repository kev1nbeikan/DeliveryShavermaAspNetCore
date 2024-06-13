using Handler.Core;
using HandlerService.Contracts;
using HandlerService.Controllers;

namespace HandlerService.Models;

public class PaymentViewModel
{
    public PaymentRequest PaymentRequest { get; set; }
    public long Price { get; set; }

    public List<PaymentType> PaymentTypes { get; set; }
    public Order? Order { get; set; }
    public Product[] Products { get; set; }
}