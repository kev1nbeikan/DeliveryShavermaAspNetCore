
using AuthService.Application.Utils;

namespace AuthService.UnitTests.Fixtures;

public class PasswordHashFixture
{
    public static readonly PasswordHasher PasswordHasher;

    static PasswordHashFixture()
    {
        PasswordHasher = new PasswordHasher();
    }
}