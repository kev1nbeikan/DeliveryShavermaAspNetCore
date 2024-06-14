using Handler.Core;
using Handler.Core.Abstractions;
using HandlerService.Infustucture.Extensions;

namespace HandlerService.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string?> Save(Guid userId, string address, string comment)
    {
        var (user, error) = MyUser.Create(userId, [address], comment);
        return
            error.HasValue()
                ? error
                : await _userRepository.SaveByUserId(user!);
    }

    public async Task<(MyUser?, string? error)> Get(Guid userId)
    {
        var myUser = await _userRepository.Get(userId);

        if (myUser == null) return (null, "User not found");

        return (myUser, null);
    }
}