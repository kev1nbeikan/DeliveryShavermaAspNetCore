using CourierService.Core.Models.Code;

namespace CourierService.API.Models;

public class CourierViewModel
{
    public Guid Id { get; set; }

    public CourierStatusCode Status { get; set; }
}