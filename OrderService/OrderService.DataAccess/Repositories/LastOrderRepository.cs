using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;

namespace OrderService.DataAccess.Repositories;

public class LastOrderRepository(OrderServiceDbContext context)
{
    private readonly OrderServiceDbContext _context = context;

    public async Task<List<LastOrder>> Get()
    {
        var orderEntity = await _context.LastOrders
            .AsNoTracking()
            .ToListAsync();
        
        var orders = orderEntity.Select(b => LastOrder.Create(
                b.Id,
                b.ClientId,
                b.CourierId,
                b.StoreId,
                b.Basket,
                b.Price,
                b.Comment,
                b.ClientAddress,
                b.CourierNumber,
                b.ClientNumber,
                b.CookingTime,
                b.DeliveryTime,
                b.OrderDate,
                b.CookingDate,
                b.DeliveryDate,
                b.Cheque
                ).Order)
            .ToList();
        return orders;
    }
    
    public async Task<LastOrder> Get(Guid id)
    {
        var orderEntity = await _context.LastOrders
                              .AsNoTracking()
                              .FirstOrDefaultAsync(b => b.Id == id)
                          ?? throw new KeyNotFoundException();

        var order = LastOrder.Create(
            orderEntity.Id,
            orderEntity.ClientId,
            orderEntity.CourierId,
            orderEntity.StoreId,
            orderEntity.Basket,
            orderEntity.Price,
            orderEntity.Comment,
            orderEntity.ClientAddress,
            orderEntity.CourierNumber,
            orderEntity.ClientNumber,
            orderEntity.CookingTime,
            orderEntity.DeliveryTime,
            orderEntity.OrderDate,
            orderEntity.CookingDate,
            orderEntity.DeliveryDate,
            orderEntity.Cheque
            ).Order;

        return order;
    }

    public async Task Create(LastOrder order)
    {
        var orderEntity = new LastOrderEntity
        {
            Id = order.Id,
            ClientId = order.ClientId,
            CourierId = order.CourierId,
            StoreId = order.StoreId,
            Basket = order.Basket,
            Price = order.Price,
            Comment = order.Comment,
            ClientAddress = order.ClientAddress,
            CourierNumber = order.CourierNumber,
            ClientNumber = order.ClientNumber,
            CookingTime = order.CookingTime,
            DeliveryTime = order.DeliveryTime,
            OrderDate = DateTime.UtcNow,
            CookingDate = DateTime.UtcNow,
            DeliveryDate = DateTime.UtcNow,
            Cheque =  order.Cheque
        };

        await _context.LastOrders.AddAsync(orderEntity);
        await _context.SaveChangesAsync();
    }
    
    // public async Task Delete(Guid id)
    // {
    //     await _context.CurrentOrders
    //         .Where(b => b.Id == id)
    //         .ExecuteDeleteAsync();
    // }
}