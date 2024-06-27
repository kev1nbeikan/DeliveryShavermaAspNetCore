using AuthService.Core.Abstractions;
using AuthService.Core.Exceptions;
using AuthService.DataAccess.Entities;
using User.Infastructure.Abstractions;

namespace AuthService.Application.Services;

public class StoreAuthService : IStoreAuthService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IStoreAuthRepo _storeAuthRepository;

    public StoreAuthService(IPasswordHasher passwordHasher, IStoreAuthRepo storeAuthRepository)
    {
        _passwordHasher = passwordHasher;
        _storeAuthRepository = storeAuthRepository;
    }

    public async Task<Guid> Register(string login, string password)
    {
        var store = await _storeAuthRepository.GetByLogin(login);

        if (store != null)
        {
            throw new UniqeConstraitException("store with this login already exists");
        }

        var hashedPassword = _passwordHasher.Generate(password);

        var newStore = StoreAuth.Create(Guid.NewGuid(), login, hashedPassword);

        await _storeAuthRepository.Add(newStore);

        return newStore.Id;
    }

    public async Task<Guid> Login(string login, string password)
    {
        var store = await _storeAuthRepository.GetByLogin(login);

        if (store == null)
        {
            throw new NotFoundException("store not found");
        }

        if (!_passwordHasher.Verify(password, store.PasswordHash))
        {
            throw new IncorectPasswordException(store.Login);
        }

        return store.Id;
    }

    public async Task<StoreAuth> GetStore(Guid id)
    {
        var store = await _storeAuthRepository.GetById(id);

        if (store == null)
        {
            throw new NotFoundException("store not found");
        }

        return store;
    }
}