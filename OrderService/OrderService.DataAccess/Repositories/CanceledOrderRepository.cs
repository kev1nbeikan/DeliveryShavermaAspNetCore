using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;

namespace OrderService.DataAccess.Repositories;

public class CanceledOrderRepository(OrderServiceDbContext context)
{
    private readonly OrderServiceDbContext _context = context;

    public async Task<List<CanceledOrder>> Get()
    {
        var orderEntity = await _context.CanceledOrders
            .AsNoTracking()
            .ToListAsync();
        
        var orders = orderEntity.Select(b => CanceledOrder.Create(
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
                b.Cheque,
                (StatusCode)b.LastStatus,
                b.ReasonOfCanceled
                ).Order)
            .ToList();
        return orders;
    }
    
    public async Task Create(CanceledOrder order)
    {
        var orderEntity = new CanceledOrderEntity
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
            Cheque =  order.Cheque,
            LastStatus = (int)order.LastStatus,
            ReasonOfCanceled = order.ReasonOfCanceled
        };

        await _context.CanceledOrders.AddAsync(orderEntity);
        await _context.SaveChangesAsync();
    }
    
    // public async Task<CanceledOrder> Get(Guid id)
    // {
    //     var orderEntity = await _context.CanceledOrders
    //                           .AsNoTracking()
    //                           .FirstOrDefaultAsync(b => b.Id == id)
    //                       ?? throw new KeyNotFoundException();
    //
    //     var order = CanceledOrder.Create(
    //         orderEntity.Id,
    //         orderEntity.ClientId,
    //         orderEntity.CourierId,
    //         orderEntity.StoreId,
    //         orderEntity.Basket,
    //         orderEntity.Price,
    //         orderEntity.Comment,
    //         orderEntity.ClientAddress,
    //         orderEntity.CourierNumber,
    //         orderEntity.ClientNumber,
    //         orderEntity.CookingTime,
    //         orderEntity.DeliveryTime,
    //         orderEntity.OrderDate,
    //         orderEntity.CookingDate,
    //         orderEntity.DeliveryDate,
    //         orderEntity.Cheque,
    //         (StatusCode)orderEntity.LastStatus,
    //         orderEntity.ReasonOfCanceled
    //         ).Order;
    //     return order;
    // }
}
