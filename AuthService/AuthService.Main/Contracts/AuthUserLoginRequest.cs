using System.ComponentModel.DataAnnotations;

namespace AuthService.Main.Contracts;

public class AuthUserLoginRequest
{
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
}