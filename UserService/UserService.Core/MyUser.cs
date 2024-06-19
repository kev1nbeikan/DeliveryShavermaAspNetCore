namespace UserService.Core;

public class MyUser
{
    public Guid UserId { get; set; }
    public List<string> Addresses { get; set; }
    public string Comment { get; set; }
    public string PhoneNumber { get; set; }

    public MyUser(Guid userId, List<string> address, string comment, string phoneNumber)
    {
        UserId = userId;
        Addresses = address;
        Comment = comment;
        PhoneNumber = phoneNumber;
    }


    public static (MyUser? myUser, string? error) Create(Guid userId, List<string> address,
        string comment, string phoneNumber = "")
    {
        string? error = null;

        return (new MyUser(userId!, address!, comment!, phoneNumber), error);
    }
    
}