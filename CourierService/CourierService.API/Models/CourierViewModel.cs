using BarsGroupProjectN1.Core.Models.Courier;
using CourierService.Core.Models.Code;

namespace CourierService.API.Models;

public class CourierViewModel
{
    public Guid Id { get; set; }

    public CourierStatusCode Status { get; set; }
    public int ActiveOrdersCount { get; set; }
}