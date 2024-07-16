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
public class LastOrderRepository(OrderServiceDbContext context) : ILastOrderRepository
{
    /// <summary>
    /// Возвращает список прошлых заказов для указанной роли и идентификатора источника.
    /// </summary>
    /// <param name="role">Роль пользователя.</param>
    /// <param name="sourceId">Идентификатор источника (клиент, курьер, магазин).</param>
    /// <returns>Список прошлых заказов.</returns>
    /// <exception cref="FailToUseOrderRepository">Исключение выбрасывается, при работе json.</exception>
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
                ?? throw new FailToUseOrderRepository("Корзина не может быть преобразована из json"),
                b.Price,
                b.Comment,
                b.CookingTime,
                b.DeliveryTime,
                b.OrderDate,
                b.CookingDate,
                b.DeliveryDate,
                b.Cheque
            ))
            .ToList();
        return orders;
    }

    /// <summary>
    /// Создает новый заказ в историю.
    /// </summary>
    /// <param name="order">Объект заказа.</param>
    /// <exception cref="FailToUseOrderRepository">Исключение выбрасывается, при работе json.</exception>
    public async Task Create(CurrentOrder order)
    {
        var orderEntity = new LastOrderEntity
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
            Cheque = order.Cheque
        };

        await context.LastOrders.AddAsync(orderEntity);
        await context.SaveChangesAsync();
    }
}