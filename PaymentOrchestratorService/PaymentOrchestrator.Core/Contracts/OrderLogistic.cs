using BarsGroupProjectN1.Core.Contracts;
using Handler.Core.Common;

namespace Handler.Core.Contracts;

public record OrderLogistic
{
    public OrderTaskExecution<Curier> Delivery { get; set; } = new();
    public OrderTaskExecution<Guid> Cooking { get; set; } = new();
}