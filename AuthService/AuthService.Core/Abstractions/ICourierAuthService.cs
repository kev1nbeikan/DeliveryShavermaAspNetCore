namespace AuthService.Core.Abstractions;

public interface ICourierAuthService
{
	Task<CourierAuth> Register(string login, string password);
	Task<Guid> Login(string login, string password);
	Task<CourierAuth> GetCourier(Guid id);
}