using UserService.Core;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Extentions;

public static class MyUserExtensions
{
    public static UserEntity ToUserEntity(this MyUser myUser)
    {
        return new UserEntity
        {
            Id = myUser.UserId,
            Addresses = myUser.Addresses.ToAddressEntity(myUser.UserId),
            Comment = myUser.Comment,
            PhoneNumber = myUser.PhoneNumber
        };
    }

    private static List<AddressEntity> ToAddressEntity(this List<string> addresses, Guid userId)
    {
        return addresses.Select(x => new AddressEntity
        {
            Address = x,
            UserEntityId = userId
        }).ToList();
    }
}