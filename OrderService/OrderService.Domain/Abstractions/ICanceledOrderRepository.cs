using OrderService.Domain.Models.Code;
using OrderService.Domain.Models.Order;

namespace OrderService.Domain.Abstractions;

public interface ICanceledOrderRepository
{
	Task<List<CanceledOrder>> Get(RoleCode role, Guid sourceId);
	Task Create(CurrentOrder order, string reasonOfCanceled);
}