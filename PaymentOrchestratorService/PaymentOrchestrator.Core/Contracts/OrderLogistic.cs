using BarsGroupProjectN1.Core.Contracts;
using BarsGroupProjectN1.Core.Models.Store;
using Handler.Core.Common;

namespace Handler.Core.Contracts;

public record OrderLogistic
{
    public OrderTaskExecution<Curier> Delivering { get; set; } = new();
    public OrderTaskExecution<Store> Cooking { get; set; } = new();
}