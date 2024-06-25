namespace Handler.Core.Common;

public class Curier
{
    public Guid Id { get; set; }
    public String PhoneNumber { get; set; }

    public Curier(Guid id, string phoneNumber)
    {
        Id = id;
        PhoneNumber = phoneNumber;
    }
}