﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;
using OrderService.Domain.Abstractions;

namespace OrderService.DataAccess.Repositories;

public class CurrentOrderRepository(OrderServiceDbContext context) : ICurrentOrderRepository
{
    public async Task<List<CurrentOrder>> Get(RoleCode role, Guid sourceId)
    {
        var condition = BaseOrderRepository.GetCondition<CurrentOrderEntity>(role, sourceId);

        var orderEntity = await context.CurrentOrders
            .AsNoTracking()
            .Where(condition)
            .ToListAsync();

        var orders = orderEntity.Select(b => CurrentOrder.Create(
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
                (StatusCode)b.Status).Order)
            .ToList();
        return orders;
    }

    public async Task<StatusCode> GetStatus(RoleCode role, Guid sourceId, Guid id)
    {
        var condition = BaseOrderRepository.GetCondition<CurrentOrderEntity>(role, sourceId);

        var orderEntity = await context.CurrentOrders
                              .AsNoTracking()
                              .Where(condition)
                              .FirstOrDefaultAsync(b => b.Id == id)
                          ?? throw new KeyNotFoundException();
        return (StatusCode)orderEntity.Status;
    }

    public async Task ChangeStatus(RoleCode role, StatusCode status, Guid sourceId, Guid id)
    {
        var condition = BaseOrderRepository.GetCondition<CurrentOrderEntity>(role, sourceId);

        await context.CurrentOrders
            .Where(b => b.Id == id)
            .Where(condition)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Status, b => (int)status));
    }

    public async Task ChangeCookingDate(RoleCode role, DateTime cookingDate, Guid sourceId, Guid id)
    {
        var condition = BaseOrderRepository.GetCondition<CurrentOrderEntity>(role, sourceId);

        await context.CurrentOrders
            .Where(b => b.Id == id)
            .Where(condition)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.CookingDate, b => cookingDate));
    }

    public async Task ChangeDeliveryDate(RoleCode role, DateTime deliveryDate, Guid sourceId, Guid id)
    {
        var condition = BaseOrderRepository.GetCondition<CurrentOrderEntity>(role, sourceId);

        await context.CurrentOrders
            .Where(b => b.Id == id)
            .Where(condition)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.DeliveryDate, b => deliveryDate));
    }
    
    public async Task Delete(RoleCode role, Guid sourceId, Guid id)
    {
        var condition = BaseOrderRepository.GetCondition<CurrentOrderEntity>(role, sourceId);

        await context.CurrentOrders
            .Where(b => b.Id == id)
            .Where(condition)
            .ExecuteDeleteAsync();
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
            Price = order.Price,
            Comment = order.Comment,
            ClientAddress = order.ClientAddress,
            CourierNumber = order.CourierNumber,
            ClientNumber = order.ClientNumber,
            CookingTime = order.CookingTime,
            DeliveryTime = order.DeliveryTime,
            OrderDate = order.OrderDate,
            CookingDate = order.CookingDate,
            DeliveryDate = order.DeliveryDate,
            Cheque = order.Cheque,
            Status = (int)order.Status
        };

        await context.CurrentOrders.AddAsync(orderEntity);
        await context.SaveChangesAsync();
    }
}