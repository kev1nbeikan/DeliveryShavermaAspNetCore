using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Models.Code;
using OrderService.Domain.Models.Order;

namespace OrderService.DataAccess.Repositories;

public class CanceledOrderRepository(OrderServiceDbContext context) : ICanceledOrderRepository
{
    public async Task<List<CanceledOrder>> Get(RoleCode role, Guid sourceId)
    {
        var condition = BaseOrderRepository.GetCondition<CanceledOrderEntity>(role, sourceId);

        var orderEntity = await context.CanceledOrders
            .AsNoTracking()
            .Where(condition)
            .ToListAsync();

        var orders = orderEntity.Select(b => CanceledOrder.Create(
                b.Id,
                b.ClientId,
                b.CourierId,
                b.StoreId,
                JsonSerializer.Deserialize<List<BasketItem>>(b.Basket) 
                ?? throw new ArgumentException("Basket cannot be null", nameof(orderEntity)),
                b.Price,
                b.Comment,
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

    public async Task Create(CurrentOrder order, string reasonOfCanceled)
    {
        var orderEntity = new CanceledOrderEntity
        {
            Id = order.Id,
            ClientId = order.ClientId,
            CourierId = order.CourierId,
            StoreId = order.StoreId,
            Basket = JsonSerializer.Serialize(order.Basket),
            Price = order.Price,
            Comment = order.Comment,
            CookingTime = order.CookingTime,
            DeliveryTime = order.DeliveryTime,
            OrderDate = order.OrderDate,
            CookingDate = order.CookingDate,
            DeliveryDate = order.DeliveryDate,
            Cheque = order.Cheque,
            LastStatus = (int)order.Status,
            ReasonOfCanceled = reasonOfCanceled
        };

        await context.CanceledOrders.AddAsync(orderEntity);
        await context.SaveChangesAsync();
    }
}