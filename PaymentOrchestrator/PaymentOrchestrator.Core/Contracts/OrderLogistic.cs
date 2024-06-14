using HandlerService.Controllers;

namespace Handler.Core.Contracts;

public class OrderLogistic
{
    public OrderLogistic()
    {
        Delivery = new OrderTaskInfo<Curier>();
        Cooking = new OrderTaskInfo<Guid>();
    }

    public OrderTaskInfo<Curier> Delivery { get; set; }
    public OrderTaskInfo<Guid> Cooking { get; set; }
}

public class OrderTaskInfo<T>
{
    public T? Perfomer { get; set; }
    public TimeSpan Time { get; set; }
}