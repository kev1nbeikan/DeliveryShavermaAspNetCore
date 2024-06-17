using OrderService.DataAccess.Repositories;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Models;

namespace OrderService.Application.Service;

public class CurrentOrderService(ICurrentOrderRepository currentOrderRepository, ILastOrderRepository lastOrderRepository, ICanceledOrderRepository canceledOrderRepository)
{
    private readonly ICurrentOrderRepository _currentOrderRepository = currentOrderRepository;
    private readonly ILastOrderRepository _lastOrderRepository = lastOrderRepository;
    private readonly ICanceledOrderRepository _canceledOrderRepository = canceledOrderRepository; 
    
    public async Task<List<CurrentOrder>> GetCurrentOrders(RoleCode role, Guid id)
    {
        return await _currentOrderRepository.Get(role, id);
    }
    
    public async Task<CurrentOrder> GetNewestOrder(RoleCode role, Guid id)
    {
        var orders = await _currentOrderRepository.Get(role, id);
        var newestOrder = orders.FirstOrDefault(x => x.OrderDate == orders.Max(o => o.OrderDate))
            ?? throw new InvalidOperationException();
        return newestOrder;
    }
    public async Task<List<LastOrder>> GetLastOrders(RoleCode role, Guid id)
    {
        return await _lastOrderRepository.Get(role, id);
    }
    
    public async Task<List<CanceledOrder>> GetCanceledOrders(RoleCode role, Guid id)
    {
        return await _canceledOrderRepository.Get(role, id);
    }
    
    public async Task ChangeStatus (Guid sourceId, Guid id, RoleCode role, StatusCode status)
    {
        await _currentOrderRepository.ChangeStatus(id, status);
        if (status == StatusCode.WaitingCourier)
            await _currentOrderRepository.ChangeCookingDate(id, DateTime.UtcNow);
        if (status == StatusCode.WaitingClient)
            await _currentOrderRepository.ChangeDeliveryDate(id, DateTime.UtcNow);
    }
    
    public async Task<StatusCode> GetStatus(Guid id)
    {
        return await _currentOrderRepository.GetStatus(id);
    }
}