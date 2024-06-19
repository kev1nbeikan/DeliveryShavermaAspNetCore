using AuthService.Core.Common;

namespace AuthService.Main.Contracts;

public class LoginResponse
{
    public string? UserId { get; set; }
    public RoleCode? Role { get; set; }
}