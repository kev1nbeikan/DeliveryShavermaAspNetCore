using System.Runtime.CompilerServices;
using UserService.Core;

namespace UserService.Application;

public static class MyUserExtensions
{
    public static MyUser MergeWith(this MyUser user, MyUser newFields)
    {
        user.Addresses.AddRange(newFields.Addresses);
        user.Comment = newFields.Comment;
        user.PhoneNumber = newFields.PhoneNumber;
        return user;
    }
}