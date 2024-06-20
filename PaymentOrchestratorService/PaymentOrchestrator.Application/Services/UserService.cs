using Handler.Core;
using Handler.Core.Abstractions;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Common;
using Handler.Core.Contracts;
using HandlerService.Infustucture.Extensions;

namespace HandlerService.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string?> AddNewOrUpdate(Guid userId, string address, string comment, string phoneNumber)
    {
        var (user, error) = MyUser.Create(userId, [address], comment, phoneNumber);
        AddNewUserOrUpdateUserFields fields = new(userId, address, comment, phoneNumber);
        


        return
            error.HasValue()
                ? error
                : await _userRepository.AddNewOrUpdate(fields);
    }

    public async Task<(MyUser?, string? error)> Get(Guid userId)
    {
        var myUser = await _userRepository.Get(userId);

        if (myUser == null) return (null, "User not found");

        return (myUser, null);
    }
}