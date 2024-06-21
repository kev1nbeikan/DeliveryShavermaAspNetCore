using UserService.Core;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Extensions;

public static class EntitiesConvertingExtensions
{
    public static UserEntity ToUserEntity(this MyUser myUser)
    {
        return new UserEntity
        {
            Id = myUser.UserId,
            Addresses = myUser.Addresses.ToAddressEntities(myUser.UserId),
            Comment = myUser.Comment,
            PhoneNumber = myUser.PhoneNumber
        };
    }

    public static List<AddressEntity> ToAddressEntities(this List<string> addresses, Guid userId)
    {
        return addresses.Select(x => x.ToAddressEntity(userId)).ToList();
    }

    public static AddressEntity ToAddressEntity(this string addresss, Guid userId)
    {
        return new AddressEntity
        {
            Address = addresss,
            UserEntityId = userId
        };
    }
}