using OrderService.Domain.Models.Code;
using OrderService.Domain.Models.Order;

namespace OrderService.Domain.Abstractions;

public interface IOrderApplicationService
{
    Task<List<CurrentOrder>> GetCurrentOrders(RoleCode role, Guid sourceId);
    Task<List<LastOrder>> GetLastOrders(RoleCode role, Guid sourceId);
    Task<List<CanceledOrder>> GetCanceledOrders(RoleCode role, Guid sourceId);
    Task<CurrentOrder> GetOldestActive(RoleCode role, Guid sourceId);
    Task ChangeStatusActive(RoleCode role, StatusCode status, Guid sourceId, Guid orderId);
    Task ChangeStatusCompleted(RoleCode role, Guid sourceId, Guid orderId);
    Task ChangeStatusCanceled(RoleCode role, Guid sourceId, Guid orderId, string reasonOfCanceled);
    Task<List<CurrentOrder>> GetNewOrdersByDate(RoleCode role, Guid sourceId, DateTime lastOrderDate);
    Task<StatusCode> GetStatus(RoleCode role, Guid sourceId, Guid id);
    Task CreateCurrentOrder(CurrentOrder order);
}