using System.ComponentModel.DataAnnotations;

namespace AuthService.Main.Contracts;

public class AuthStaffRequest
{
    [Required] public string Login { get; set; }
    [Required] public string Password { get; set; }
}