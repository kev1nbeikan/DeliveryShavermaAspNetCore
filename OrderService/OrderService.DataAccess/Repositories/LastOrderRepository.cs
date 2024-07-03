using System.Text.Json;
using BarsGroupProjectN1.Core.Models.Order;
using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Common;
using OrderService.Domain.Common.Code;
using OrderService.Domain.Models;

namespace OrderService.DataAccess.Repositories;

public class LastOrderRepository(OrderServiceDbContext context) : ILastOrderRepository
{
    public async Task<List<LastOrder>> Get(RoleCode role, Guid sourceId)
    {
        var condition = BaseOrderRepository.GetCondition<LastOrderEntity>(role, sourceId);

        var orderEntity = await context.LastOrders
            .AsNoTracking()
            .Where(condition).ToListAsync();

        var orders = orderEntity.Select(b => LastOrder.Create(
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
            Basket = JsonSerializer.Serialize(order.Basket),
            Price = order.Price,
            Comment = order.Comment,
            CookingTime = order.CookingTime,
            DeliveryTime = order.DeliveryTime,
            OrderDate = order.OrderDate,
            CookingDate = order.CookingDate,
            DeliveryDate = order.DeliveryDate,
            Cheque = order.Cheque
        };

        await context.LastOrders.AddAsync(orderEntity);
        await context.SaveChangesAsync();
    }
}