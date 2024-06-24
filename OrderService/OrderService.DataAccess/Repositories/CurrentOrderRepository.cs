using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Models.Code;
using OrderService.Domain.Models.Order;

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
                BasketConverter.ToBasketItem(b.Basket),
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
            BasketConverter.ToBasketItem(orderEntity.Basket),
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
        if (!Enum.IsDefined(typeof(StatusCode), status))
            throw new ArgumentException("Invalid status value.", nameof(status));

        var condition = BaseOrderRepository.GetCondition<CurrentOrderEntity>(role, sourceId);

        var order = await context.CurrentOrders
            .Where(condition)
            .FirstOrDefaultAsync(b => b.Id == id) ?? throw new KeyNotFoundException();
        
        if ((int)status <= order.Status)
            throw new ArgumentException("New status cannot be less than or equal to the current status.");
        
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
            Basket = order.Basket,
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
}