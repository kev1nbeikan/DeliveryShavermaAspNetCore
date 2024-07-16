using System.Text.Json;
using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Models;

namespace OrderService.DataAccess.Repositories;

///<inheritdoc/>
/// <param name="context">Контекст БД.</param>
public class CanceledOrderRepository(OrderServiceDbContext context) : ICanceledOrderRepository
{
    /// <summary>
    /// Возвращает список отмененных заказов для указанной роли и идентификатора источника.
    /// </summary>
    /// <param name="role">Роль пользователя.</param>
    /// <param name="sourceId">Идентификатор источника (клиент, курьер, магазин).</param>
    /// <returns>Список отмененных заказов.</returns>
    /// <exception cref="FailToUseOrderRepository">Исключение выбрасывается, при работе json.</exception>
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
                ?? throw new FailToUseOrderRepository("Корзина не может быть преобразована из json"),
                b.Price,
                b.Comment,
                b.CookingTime,
                b.DeliveryTime,
                b.OrderDate,
                b.CookingDate,
                b.DeliveryDate,
                b.Cheque,
                (StatusCode)b.LastStatus,
                b.ReasonOfCanceled,
                b.CanceledDate,
                role
            ))
            .ToList();
        return orders;
    }

    /// <summary>
    /// Создает новый отмененный заказ.
    /// </summary>
    /// <param name="order">Объект заказа.</param>
    /// <param name="reasonOfCanceled">Причина отмены заказа.</param>
    /// <param name="role">Роль пользователя, который отменил заказ.</param>
    /// <exception cref="FailToUseOrderRepository">Исключение выбрасывается, при работе json.</exception>
    public async Task Create(CurrentOrder order, string reasonOfCanceled, RoleCode role)
    {
        var orderEntity = new CanceledOrderEntity
        {
            Id = order.Id,
            ClientId = order.ClientId,
            CourierId = order.CourierId,
            StoreId = order.StoreId,
            Basket = JsonSerializer.Serialize(order.Basket)
                     ?? throw new FailToUseOrderRepository($"Json не может быть преобразован в корзину."),
            Price = order.Price,
            Comment = order.Comment,
            CookingTime = order.CookingTime,
            DeliveryTime = order.DeliveryTime,
            OrderDate = order.OrderDate,
            CookingDate = order.CookingDate,
            DeliveryDate = order.DeliveryDate,
            Cheque = order.Cheque,
            LastStatus = (int)order.Status,
            ReasonOfCanceled = reasonOfCanceled,
            CanceledDate = DateTime.UtcNow,
            WhoCanceled = (int)role
        };

        await context.CanceledOrders.AddAsync(orderEntity);
        await context.SaveChangesAsync();
    }
}