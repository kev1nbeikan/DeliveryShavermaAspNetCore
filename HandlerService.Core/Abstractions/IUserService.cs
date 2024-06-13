namespace Handler.Core.Abstractions;

public interface IUserService
{
    (MyUser? myUser, string? error) Save(Guid userId, string paymentRequestAddress, string paymentRequestComment);
}