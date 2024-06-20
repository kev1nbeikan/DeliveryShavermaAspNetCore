using OrderService.Domain.Abstractions;
using OrderService.Domain.Models;
using OrderService.Domain.Models.Code;

namespace OrderService.Application.Service;

public class OrderApplicationService(
    ICurrentOrderRepository currentOrderRepository,
    ILastOrderRepository lastOrderRepository,
    ICanceledOrderRepository canceledOrderRepository) : IOrderApplicationService
{
    private readonly ICurrentOrderRepository _currentOrderRepository = currentOrderRepository;
    private readonly ILastOrderRepository _lastOrderRepository = lastOrderRepository;
    private readonly ICanceledOrderRepository _canceledOrderRepository = canceledOrderRepository;

    public async Task<List<CurrentOrder>> GetCurrentOrders(RoleCode role, Guid sourceId)
    {
        return await _currentOrderRepository.Get(role, sourceId);
    }

    public async Task<List<LastOrder>> GetLastOrders(RoleCode role, Guid sourceId)
    {
        return await _lastOrderRepository.Get(role, sourceId);
    }

    public async Task<List<CanceledOrder>> GetCanceledOrders(RoleCode role, Guid sourceId)
    {
        return await _canceledOrderRepository.Get(role, sourceId);
    }

    public async Task<CurrentOrder> GetNewestOrder(RoleCode role, Guid sourceId)
    {
        var orders = await _currentOrderRepository.Get(role, sourceId);
        var newestOrder = orders.FirstOrDefault(x =>
                              x.OrderDate == orders.Max(o => o.OrderDate))
                          ?? throw new InvalidOperationException();
        return newestOrder;
    }

    public async Task ChangeStatusActive(RoleCode role, StatusCode status, Guid sourceId, Guid orderId)
    {
        await _currentOrderRepository.ChangeStatus(role, status, sourceId, orderId);
        if (status == StatusCode.WaitingCourier)
            await _currentOrderRepository.ChangeCookingDate(role, DateTime.UtcNow, sourceId, orderId);
        if (status == StatusCode.WaitingClient)
            await _currentOrderRepository.ChangeDeliveryDate(role, DateTime.UtcNow, sourceId, orderId);
    }

    public async Task ChangeStatusCompleted(RoleCode role, Guid sourceId, Guid orderId)
    {
        var order = await _currentOrderRepository.GetById(role, sourceId, orderId);
        await _lastOrderRepository.Create(order);
        await _currentOrderRepository.Delete(role, sourceId, orderId);
    }

    public async Task ChangeStatusCanceled(RoleCode role, Guid sourceId, Guid orderId, string reasonOfCanceled)
    {
        var order = await _currentOrderRepository.GetById(role, sourceId, orderId);
        await _canceledOrderRepository.Create(order, reasonOfCanceled);
        await _currentOrderRepository.Delete(role, sourceId, orderId);
    }

    public async Task<List<CurrentOrder>> GetNewOrdersByDate(RoleCode role, Guid sourceId, DateTime lastOrderDate)
    {
        var orders = await _currentOrderRepository.Get(role, sourceId);
        var newestOrder = orders.Where(x => x.OrderDate > lastOrderDate)
            .OrderBy(x => x.OrderDate)
            .ToList();
        return newestOrder;
    }

    public async Task<StatusCode> GetStatus(RoleCode role, Guid sourceId, Guid id)
    {
        return await _currentOrderRepository.GetStatus(role, sourceId, id);
    }

    public async Task CreateCurrentOrder(CurrentOrder order)
    {
        await _currentOrderRepository.Create(order);
    }
}