using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Common;
using OrderService.Domain.Common.Code;
using OrderService.Domain.Models;

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
                JsonSerializer.Deserialize<List<BasketItem>>(b.Basket)
                ?? throw new ArgumentException("Basket cannot be null", nameof(orderEntity)),
                b.Price,
                b.Comment,
                b.StoreAddress,
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

    public async Task<CurrentOrder> GetById(RoleCode role, Guid sourceId, Guid id)
    {
        var condition = BaseOrderRepository.GetCondition<CurrentOrderEntity>(role, sourceId);

        var orderEntity = await context.CurrentOrders
                              .AsNoTracking()
                              .Where(condition)
                              .FirstOrDefaultAsync(b => b.Id == id)
                          ?? throw new KeyNotFoundException();

        var order = CurrentOrder.Create(
            orderEntity.Id,
            orderEntity.ClientId,
            orderEntity.CourierId,
            orderEntity.StoreId,
            JsonSerializer.Deserialize<List<BasketItem>>(orderEntity.Basket)
            ?? throw new ArgumentException("Basket cannot be null", nameof(orderEntity)),
            orderEntity.Price,
            orderEntity.Comment,
            orderEntity.StoreAddress,
            orderEntity.ClientAddress,
            orderEntity.CourierNumber,
            orderEntity.ClientNumber,
            orderEntity.CookingTime,
            orderEntity.DeliveryTime,
            orderEntity.OrderDate,
            orderEntity.CookingDate,
            orderEntity.DeliveryDate,
            orderEntity.Cheque,
            (StatusCode)orderEntity.Status).Order;

        return order;
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

        var order = await context.CurrentOrders
            .Where(condition)
            .FirstOrDefaultAsync(b => b.Id == id) ?? throw new KeyNotFoundException();

        ValidateStatus(status, order);
        
        order.Status = (int)status;

        await context.SaveChangesAsync();
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
            Basket = JsonSerializer.Serialize(order.Basket),
            Price = order.Price,
            Comment = order.Comment,
            StoreAddress = order.StoreAddress,
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
    
    private void ValidateStatus(StatusCode status, CurrentOrderEntity order)
    {
        if (!Enum.IsDefined(typeof(StatusCode), status))
            throw new ArgumentException($"Invalid status value. Current status: {(StatusCode)order.Status}, new status: {status}", nameof(status));
        if ((int)status <= order.Status)
            throw new ArgumentException($"New status cannot be less than or equal to the current status. Current status: {(StatusCode)order.Status}, new status: {status}", nameof(status));
        if ((int)status - 1 != order.Status)
            throw new ArgumentException($"New status skips previous states. Current status: {(StatusCode)order.Status}, new status: {status}", nameof(status));
    }
}