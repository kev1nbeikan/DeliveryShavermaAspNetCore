using OrderService.Domain.Models.Code;
using OrderService.Domain.Models.Order;

namespace OrderService.Domain.Abstractions;

public interface ILastOrderRepository
{
    Task<List<LastOrder>> Get(RoleCode role, Guid sourceId);
    Task Create(CurrentOrder order);
}