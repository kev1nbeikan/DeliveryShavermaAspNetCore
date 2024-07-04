using CourierService.Core.Models.Code;

namespace CourierService.Core.Models;

public class Courier
{
	private Courier(Guid id, CourierStatusCode status)
	{
		Id = id;
		Status = status;
	}

	public Guid Id { get; }

	public CourierStatusCode Status { get; }

	public static (Courier Courier, string Error) Create(
		Guid id,
		CourierStatusCode status)
	{
		var error = string.Empty;

		var courier = new Courier(id, status);

		return (courier, error);
	}
}