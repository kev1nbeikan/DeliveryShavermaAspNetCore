using System.Diagnostics.CodeAnalysis;

namespace UserService.DataAccess.Entities;

public class AddressEntity
{
    public long Id { get; set; }
    public string Address { get; set; }
    public Guid UserEntityId { get; set; }
}