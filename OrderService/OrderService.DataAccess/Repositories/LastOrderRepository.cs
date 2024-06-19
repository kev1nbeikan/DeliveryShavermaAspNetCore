using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Models.Code;

namespace OrderService.DataAccess.Repositories;

public class LastOrderRepository(OrderServiceDbContext context) : ILastOrderRepository
{
    public async Task<List<LastOrder>> Get(RoleCode role, Guid sourceId)
    {
        var condition = BaseOrderRepository.GetCondition<LastOrderEntity>(role, sourceId);

        var orderEntity = await context.LastOrders
            .AsNoTracking()
            .Where(condition).
            ToListAsync();
        
        var orders = orderEntity.Select(b => LastOrder.Create(
                b.Id,
                b.ClientId,
                b.CourierId,
                b.StoreId,
                b.Basket,
                b.Price,
                b.Comment,
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

    public async Task Create(CurrentOrder order)
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
            CookingTime = order.CookingTime,
            DeliveryTime = order.DeliveryTime,
            OrderDate = DateTime.UtcNow,
            CookingDate = DateTime.UtcNow,
            DeliveryDate = DateTime.UtcNow,
            Cheque =  order.Cheque
        };

        await context.LastOrders.AddAsync(orderEntity);
        await context.SaveChangesAsync();
    }
}