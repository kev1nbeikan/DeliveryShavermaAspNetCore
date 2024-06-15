namespace AuthService.DataAccess.Entities;

public class StoreAuth
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
}