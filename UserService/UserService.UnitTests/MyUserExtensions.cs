using UserService.Core;

namespace UserService.UnitTests;

public static class MyUserExtensions
{
    public static bool IsEqual(this MyUser user1, MyUser user2)
    {
        return user1.UserId == user2.UserId
               && user1.Comment == user2.Comment
               && user1.PhoneNumber == user2.PhoneNumber
               && user1.Addresses.OrderBy(e => e).SequenceEqual(user2.Addresses.OrderBy(e => e));
    }
}