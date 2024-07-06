using BarsGroupProjectN1.Core.Contracts;

namespace Handler.Core.Abstractions.Repositories;

public interface IOrderRepository
{
    Task<string?> Save(OrderCreateRequest order);
}