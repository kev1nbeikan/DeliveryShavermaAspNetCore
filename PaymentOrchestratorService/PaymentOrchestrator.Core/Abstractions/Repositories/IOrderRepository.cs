using BarsGroupProjectN1.Core.Contracts.Orders;

namespace Handler.Core.Abstractions.Repositories;

public interface IOrderRepository
{
    Task<string?> Save(OrderCreateRequest order);
}