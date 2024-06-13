namespace HandlerService.Controllers;

public class Curier
{
    Guid Id { get; set; }
    public String PhoneNumber { get; set; }

    public Curier(Guid id, string phoneNumber)
    {
        Id = id;
        PhoneNumber = phoneNumber;
    }
}