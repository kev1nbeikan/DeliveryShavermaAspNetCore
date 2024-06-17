using OrderService.Domain.Models;

namespace OrderService.Domain.Abstractions;

public interface ICanceledOrderRepository
{
    Task<List<CanceledOrder>> Get(RoleCode role, Guid sourceId);
    Task Create(CanceledOrder order);
}