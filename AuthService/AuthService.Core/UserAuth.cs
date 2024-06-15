namespace AuthService.Core;

public class UserAuth
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public static UserAuth Create(Guid newGuid, string email, string hashedPassword)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("email cannot be null or empty", nameof(email));
        }

        if (string.IsNullOrEmpty(hashedPassword))
        {
            throw new ArgumentException("password cannot be null or empty", nameof(hashedPassword));
        }

        if (newGuid == Guid.Empty)
        {
            throw new ArgumentException("newGuid cannot be empty", nameof(newGuid));
        }

        return new UserAuth
        {
            Id = newGuid,
            Email = email,
            PasswordHash = hashedPassword
        };
    }
}