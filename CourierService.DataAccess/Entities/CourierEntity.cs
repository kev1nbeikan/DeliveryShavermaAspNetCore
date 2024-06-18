namespace CourierService.DataAccess.Entities;

public class CourierEntity
{
	public Guid Id { get; set; }

	public string Email { get; set; }

	public string Password { get; set; }
}