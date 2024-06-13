using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;

namespace HandlerService.Application.Services;

public class OrderService : IOrderService
{
    private IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<string?> Save(Order order)
    {
        return await _orderRepository.Save(order);
    }
}