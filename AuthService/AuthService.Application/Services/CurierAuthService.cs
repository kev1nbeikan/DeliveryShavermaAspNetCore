using AuthService.Core;
using AuthService.Core.Abstractions;
using AuthService.Core.Exceptions;
using User.Infastructure.Abstractions;

namespace AuthService.Application.Services;

public class CourierAuthService : ICourierAuthService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ICourierAuthRepo _courierAuthRepository;

    public CourierAuthService(IPasswordHasher passwordHasher, ICourierAuthRepo courierAuthRepository)
    {
        _passwordHasher = passwordHasher;
        _courierAuthRepository = courierAuthRepository;
    }

    public async Task<CourierAuth> Register(string login, string password)
    {
        var courier = await _courierAuthRepository.GetByLogin(login);

        if (courier != null)
        {
            throw new UniqeConstraitException("courier with this username already exists");
        }

        var hashedPassword = _passwordHasher.Generate(password);

        var newCourier = CourierAuth.Create(Guid.NewGuid(), login, hashedPassword);

        await _courierAuthRepository.Add(newCourier);

        return newCourier;
    }

    public async Task<Guid> Login(string login, string password)
    {
        var courier = await _courierAuthRepository.GetByLogin(login);

        if (courier == null)
        {
            throw new NotFoundException("courier not found");
        }

        if (!_passwordHasher.Verify(password, courier.PasswordHash))
        {
            throw new IncorectPasswordException(courier.Login);
        }

        return courier.Id;
    }

    public async Task<CourierAuth> GetCourier(Guid id)
    {
        var courier = await _courierAuthRepository.GetById(id);

        if (courier == null)
        {
            throw new NotFoundException("courier not found");
        }

        return courier;
    }
}