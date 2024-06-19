using OrderService.Domain.Models;
using OrderService.Domain.Models.Code;

namespace OrderService.Domain.Abstractions;

public interface IOrderApplicationService
{
    Task<List<CurrentOrder>> GetCurrentOrders(RoleCode role, Guid sourceId);
    Task<List<LastOrder>> GetLastOrders(RoleCode role, Guid sourceId);
    Task<List<CanceledOrder>> GetCanceledOrders(RoleCode role, Guid sourceId);
    Task<CurrentOrder> GetNewestOrder(RoleCode role, Guid sourceId);
    Task ChangeStatusActive (RoleCode role, StatusCode status, Guid sourceId, Guid id);
    Task ChangeStatusCompleted (RoleCode role, Guid sourceId, Guid id);
    Task<List<CurrentOrder>> GetNewOrdersByDate(RoleCode role, Guid sourceId, DateTime lastOrderDate);
    Task ChangeStatusCanceled (RoleCode role, Guid sourceId, Guid id, string reasonOfCanceled);
    Task<StatusCode> GetStatus(RoleCode role, Guid sourceId, Guid id);
}