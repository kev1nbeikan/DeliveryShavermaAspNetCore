namespace CourierService.Core.Models;

public class Courier
{
	private const int MIN_PASSWORD_LENGTH = 8;

	private Courier(Guid id, string email, string password)
	{
		Id = id;
		Email = email;
		Password = password;
	}

	public Guid Id { get; }

	public string Email { get; }

	public string Password { get; }

	public static (Courier Courier, string Error) Create(Guid id, string email, string password)
	{
		var error = string.Empty;

		if (string.IsNullOrEmpty(email))
		{
			error = "Email address is null";
		}

		if (string.IsNullOrEmpty(password) || password.Length < MIN_PASSWORD_LENGTH)
		{
			error = "Password is null or password is less than 8 chars";
		}

		var courier = new Courier(id, email, password);

		return (courier, error);
	}
}