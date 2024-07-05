namespace BarsGroupProjectN1.Core.Models.Courier;

public class Courier
{
    private Courier(Guid id, CourierStatusCode status)
    {
        Id = id;
        Status = status;
    }

    private Courier(Guid id, CourierStatusCode status, int activeOrdersCount, string? phoneNumber)
    {
        Id = id;
        Status = status;
        ActiveOrdersCount = activeOrdersCount;
        PhoneNumber = phoneNumber;
    }

    public Guid Id { get; }

    public CourierStatusCode Status { get; }

    public int ActiveOrdersCount { get; set; } = 0;

    public string? PhoneNumber { get; set; } = string.Empty;

    public static (Courier Courier, string Error) Create(
        Guid id,
        CourierStatusCode status)
    {
        var error = string.Empty;

        var courier = new Courier(id, status);

        return (courier, error);
    }

    public static (Courier Courier, string Error) Create(
        Guid id,
        CourierStatusCode status,
        int activeOrdersCount,
        string phoneNumber)
    {
        var error = string.Empty;

        if (activeOrdersCount < 0) error = "Количество текуцщих заказов не должно быть меньше нуля";

        var courier = new Courier(
            id: id,
            status: status,
            activeOrdersCount: activeOrdersCount,
            phoneNumber);


        return (courier, error);
    }
}