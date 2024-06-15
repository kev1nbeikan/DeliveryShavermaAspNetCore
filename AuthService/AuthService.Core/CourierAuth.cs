namespace AuthService.Core;

public class CourierAuth
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }

    public static CourierAuth Create(Guid newGuid, string login, string hashedPassword)
    {
        
        
        if (string.IsNullOrEmpty(login))
        {
            throw new ArgumentException("login cannot be null or empty", nameof(login));
        }
        
        if (string.IsNullOrEmpty(hashedPassword))
        {
            throw new ArgumentException("password cannot be null or empty", nameof(hashedPassword));
        }
        
        if (newGuid == Guid.Empty)
        {
            throw new ArgumentException("newGuid cannot be empty", nameof(newGuid));
        }
        
        return new CourierAuth
        {
            Id = newGuid,
            Login = login,
            PasswordHash = hashedPassword
        };
    }
}