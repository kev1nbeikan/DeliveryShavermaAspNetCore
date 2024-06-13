namespace Handler.Core;

public class MyUser
{
    public Guid UserId { get; set; }
    public List<string> Addresses { get; set; }
    public string Comment { get; set; }
    public string PhoneNumber { get;  }

    private MyUser(Guid userId, List<string> address, string comment)
    {
    }

    public static (MyUser? myUser, string? error) Create(Guid userId, List<string> address,
        string comment)
    {
        string? error = null;

        return (new MyUser(userId!, address!, comment!), error);
    }
}