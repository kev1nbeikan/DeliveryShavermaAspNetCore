using OrderService.Domain.Models;

namespace OrderService.Domain.Abstractions;

public interface ILastOrderRepository
{
    Task<List<LastOrder>> Get(RoleCode role, Guid sourceId);
    Task Create(CurrentOrder order);
}