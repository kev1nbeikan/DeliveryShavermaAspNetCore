using Handler.Core;
using Handler.Core.HanlderService;
using HandlerService.Contracts;
using HandlerService.Controllers;

namespace HandlerService.Models;

public class PaymentViewModel
{
    public PaymentRequest PaymentRequest { get; set; }
    public long Price { get; set; }

    public List<PaymentType> PaymentTypes { get; set; }
    public PaymentOrder? Order { get; set; }
    public Product[] Products { get; set; }
}