using OrderService.Domain.Models;
using OrderService.Domain.Models.Code;

namespace OrderService.Domain.Abstractions;

public interface ICanceledOrderRepository
{
    Task<List<CanceledOrder>> Get(RoleCode role, Guid sourceId);
    Task Create(CurrentOrder order, string reasonOfCanceled);
}