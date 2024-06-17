using OrderService.Domain.Models;

namespace OrderService.Domain.Abstractions;

public interface ICurrentOrderRepository
{
    Task<List<CurrentOrder>> Get(RoleCode role, Guid sourceId);
    Task<CurrentOrder> GetById(RoleCode role, Guid sourceId, Guid id);
    Task<StatusCode> GetStatus(RoleCode role, Guid sourceId, Guid id);
    Task Create(CurrentOrder order);
    Task Delete(RoleCode role, Guid sourceId, Guid id);
    Task ChangeStatus(RoleCode role, StatusCode status, Guid sourceId, Guid id);
    Task ChangeCookingDate(RoleCode role, DateTime cookingDate, Guid sourceId, Guid id);
    Task ChangeDeliveryDate(RoleCode role, DateTime deliveryDate, Guid sourceId, Guid id);
}