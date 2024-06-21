using System.Runtime.CompilerServices;
using UserService.Application.Contracts;
using UserService.Core;

namespace UserService.Application;

public static class MyUserExtensions
{
    public static MyUser Update(this MyUser user, string newAddress, string newPhoneNumber,
        string newComment)
    {
        if (!user.Addresses.Contains(newAddress))
            user.Addresses.Add(newAddress);
        user.PhoneNumber = newPhoneNumber;
        user.Comment = newComment;
        return user;
    }


    public static MyUser MergeWith(this MyUser user, MyUser newFields)
    {
        user.Addresses.AddRange(newFields.Addresses);

        user.Addresses = user.Addresses.Distinct().ToList();
        user.Comment = newFields.Comment;
        user.PhoneNumber = newFields.PhoneNumber;

        return user;
    }
}