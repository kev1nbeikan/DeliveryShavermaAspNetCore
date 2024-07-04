using CourierService.Core.Models.Code;

namespace CourierService.DataAccess.Entities;

public class CourierEntity
{
	public Guid Id { get; set; }

	public CourierStatusCode Status { get; set; }
}