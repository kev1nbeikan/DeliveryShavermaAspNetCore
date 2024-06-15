using System.ComponentModel.DataAnnotations;

namespace AuthService.DataAccess.Entities;

public class StoreAuthEntity
{
    [Required] public Guid Id { get; set; }
    [Required] public string Login { get; set; }
    [Required] public string PasswordHash { get; set; }
}