namespace AuthService.Core;

public class CurierAuth
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
}