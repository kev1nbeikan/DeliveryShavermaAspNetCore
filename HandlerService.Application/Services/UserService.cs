
using Handler.Core;
using Handler.Core.Abstractions;

namespace HandlerService.Application.Services;

public class UserService : IUserService
{
    public (MyUser? myUser, string? error) Save(Guid userId, string paymentRequestAddress, string paymentRequestComment)
    {
        var (myUser, error) = MyUser.Create(userId, paymentRequestAddress, paymentRequestComment);
        if (!string.IsNullOrEmpty(error)) return (null, error);
        return (myUser, null);
    }
}