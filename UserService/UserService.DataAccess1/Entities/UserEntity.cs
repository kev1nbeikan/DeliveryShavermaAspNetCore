using System.ComponentModel.DataAnnotations;

namespace UserService.DataAccess.Entities;

public class UserEntity
{
    public Guid Id { get; set; }

    public List<AddressEntity> Addresses { get; set; }
}

