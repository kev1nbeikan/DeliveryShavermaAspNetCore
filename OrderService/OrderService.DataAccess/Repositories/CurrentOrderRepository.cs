using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;

namespace OrderService.DataAccess.Repositories;

public class CurrentOrderRepository(OrderServiceDbContext context)
{
    private readonly OrderServiceDbContext _context = context;
    
    public async Task<List<CurrentOrder>> Get()
    {
        var orderEntity = await _context.CurrentOrders
            .AsNoTracking()
            .ToListAsync();
        var orders = orderEntity.Select(b => CurrentOrder.Create(
                b.Id,
                b.ClientId,
                b.CourierId,
                b.StoreId,
                b.Basket,
                (StatusCode)b.Status,
                b.Price,
                b.Comment,
                b.ClientAddress,
                b.CourierNumber,
                b.ClientNumber,
                b.CookingTime,
                b.DeliveryTime))
            .ToList();
        return orders;
    }

    public async Task<CurrentOrder> Get(Guid id)
    {
        var orderEntity = await _context.CurrentOrders
                              .AsNoTracking()
                              .FirstOrDefaultAsync(b => b.Id == id)
                          ?? throw new KeyNotFoundException();

        var order = CurrentOrder.Create(
            orderEntity.Id,
            orderEntity.ClientId,
            orderEntity.CourierId,
            orderEntity.StoreId,
            orderEntity.Basket,
            (StatusCode)orderEntity.Status,
            orderEntity.Price,
            orderEntity.Comment,
            orderEntity.ClientAddress,
            orderEntity.CourierNumber,
            orderEntity.ClientNumber,
            orderEntity.CookingTime,
            orderEntity.DeliveryTime);

        return order;
    }

    public async Task Create(CurrentOrder order)
    {
        var orderEntity = new CurrentOrderEntity
        {
            Id = order.Id,
            ClientId = order.ClientId,
            CourierId = order.CourierId,
            StoreId = order.StoreId,
            Basket = order.Basket,
            Status = (int)order.Status,
            Price = order.Price,
            Comment = order.Comment,
            ClientAddress = order.ClientAddress,
            CourierNumber = order.CourierNumber,
            ClientNumber = order.ClientNumber,
            CookingTime = order.CookingTime,
            DeliveryTime = order.DeliveryTime
        };

        await _context.CurrentOrders.AddAsync(orderEntity);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeStatus(Guid id, StatusCode status)
    {
        await _context.CurrentOrders
            .Where(b => b.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Status, b => (int)status));
    }

    public async Task Delete(Guid id)
    {
        await _context.CurrentOrders
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();
    }
}