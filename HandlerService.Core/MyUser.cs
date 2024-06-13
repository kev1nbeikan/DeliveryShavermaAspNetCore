namespace Handler.Core;

public class MyUser
{
    private MyUser(Guid userId, string paymentRequestAddress, string s)
    {
    }

    public static (MyUser? myUser, string? error) Create(Guid userId, string paymentRequestAddress,
        string paymentRequestComment)
    {
        string? error = null;

        return (new MyUser(userId!, paymentRequestAddress!, paymentRequestAddress!), error);
    }
}