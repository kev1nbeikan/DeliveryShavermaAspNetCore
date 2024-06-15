using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Entities;

public class CurierAuthEntity
{
    [Required] public Guid Id { get; set; }
    [Required] public string Login { get; set; }
    [Required] public string PasswordHash { get; set; }
}