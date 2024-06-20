
namespace UserService.DataAccess.Entities;

public class UserEntity
{
    public Guid Id { get; set; }

    public string Comment { get; set; }

    public string PhoneNumber { get; set; }

    public List<AddressEntity> Addresses { get; set; }
}