using Handler.Core.Common;
using HandlerService.Controllers;

namespace Handler.Core.Contracts;

public record OrderLogistic
{
    public OrderTaskInfo<Curier> Delivery { get; set; } = new();
    public OrderTaskInfo<Guid> Cooking { get; set; } = new();
}

public record OrderTaskInfo<T>
{
    public T? Perfomer { get; set; }
    public TimeSpan Time { get; set; }
}