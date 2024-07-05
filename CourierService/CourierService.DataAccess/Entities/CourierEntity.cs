using BarsGroupProjectN1.Core.Models.Courier;
using CourierService.Core.Models.Code;

namespace CourierService.DataAccess.Entities;

public class CourierEntity
{
    public Guid Id { get; set; }

    public CourierStatusCode Status { get; set; }

    public int ActiveOrdersCount { get; set; } = 0;

    public string? PhoneNumber { get; set; }
}