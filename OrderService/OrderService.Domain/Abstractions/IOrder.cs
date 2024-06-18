using OrderService.Domain.Models;

namespace OrderService.Domain.Abstractions;

public interface IOrder
{
    Task<List<CurrentOrder>> GetCurrentOrders(RoleCode role, Guid sourceId);
    Task<List<LastOrder>> GetLastOrders(RoleCode role, Guid sourceId);
    Task<List<CanceledOrder>> GetCanceledOrders(RoleCode role, Guid sourceId);
    Task<CurrentOrder> GetNewestOrder(RoleCode role, Guid sourceId);
    Task ChangeStatusActive (RoleCode role, StatusCode status, Guid sourceId, Guid id);
    Task ChangeStatusCompleted (RoleCode role, Guid sourceId, Guid id);
    
    Task ChangeStatusCanceled (RoleCode role, Guid sourceId, Guid id, string reasonOfCanceled);
    Task<StatusCode> GetStatus(RoleCode role, Guid sourceId, Guid id);
}