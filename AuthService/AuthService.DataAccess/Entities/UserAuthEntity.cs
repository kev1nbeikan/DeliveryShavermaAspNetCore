namespace AuthService.DataAccess.Entities;

public class UserAuthEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}