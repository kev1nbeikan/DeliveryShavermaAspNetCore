using UserService.Core;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Extentions;

public static class UserEntityExtensions
{
    public static MyUser? ToUserEntity(this UserEntity entity)
    {
        return MyUser.Create(entity.Id, entity.Addresses.Select(x => x.Address).ToList(), entity.Comment).myUser;
    }

    public static List<string> ToStringList(this List<AddressEntity> addresses)
    {
        return addresses.Select(x => x.Address).ToList();
    }
}