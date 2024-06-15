using AuthService.Core;
using AuthService.Core.Abstractions;
using AuthService.Core.Exceptions;
using User.Infastructure.Abstractions;

namespace AuthService.Application.Services;

public class UserAuthService : IUserAuthService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserAuthRepo _userRepository;

    public UserAuthService(IPasswordHasher passwordHasher, IUserAuthRepo userRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }


    public async Task<UserAuth> Register(string userName, string email,
        string passwordHash)
    {
        var user = await _userRepository.GetByEmail(email);

        if (user != null)
        {
            throw new UniqeConstraitException("user with this email already exists");
        }

        var hashedPassword = _passwordHasher.Generate(passwordHash);

        var userAuth = UserAuth.Create(Guid.NewGuid(), email, hashedPassword);

        await _userRepository.Add(userAuth);

        return userAuth;
    }

    public async Task<Guid> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);

        if (user == null)
        {
            throw new NotFoundException("user not found");
        }

        if (!_passwordHasher.Verify(password, user.PasswordHash))
        {
            throw new IncorectPasswordException(user.Email);
        }

        return user.Id;
    }

    public async Task<UserAuth> GetUser(Guid id)
    {
        var user = await _userRepository.GetById(id);

        if (user == null)
        {
            throw new NotFoundException("user not found");
        }

        return user;
    }
}