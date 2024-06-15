using AuthService.Core;

namespace AuthService.Application.Services;

public interface ICourierAuthService
{
    Task<CourierAuth> Register(string login, string password);
    Task<Guid> Login(string login, string password);
    Task<CourierAuth> GetCourier(Guid id);
}