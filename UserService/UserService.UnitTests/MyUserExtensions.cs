using UserService.Core;

namespace UserService.UnitTests;

public static class MyUserExtensions
{
    public static bool IsEqual(this MyUser user1, MyUser user2)
    {
        return user1.UserId == user2.UserId
               && user1.Comment == user2.Comment
               && user1.Addresses.OrderBy(e => e).SequenceEqual(user1.Addresses.OrderBy(e => e));
    }
}