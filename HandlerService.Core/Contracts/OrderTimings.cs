using HandlerService.Controllers;

namespace Handler.Core.Contracts;

public class OrderTimings
{
    public OrderTimings()
    {
        DeliveryTime = new Timing<Curier>();
        CookingTime = new Timing<Guid>();
    }

    public Timing<Curier> DeliveryTime { get; set; }
    public Timing<Guid> CookingTime { get; set; }
}

public class Timing<T>
{
    public T? Agent { get; set; }
    public TimeSpan Time { get; set; }
}