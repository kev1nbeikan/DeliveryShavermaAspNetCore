using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Models;
using OrderService.Application.Extensions;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.Service;

///<inheritdoc/>

/// <param name="currentOrderRepository">Репозиторий для работы с текущими заказами.</param>
/// <param name="lastOrderRepository">Репозиторий для работы с историей заказов.</param>
/// <param name="canceledOrderRepository">Репозиторий для работы с отмененными заказами.</param>
/// <param name="orderPublisher">Сервис для публикации заказа в кафку.</param>
public class OrderApplicationService(
    ICurrentOrderRepository currentOrderRepository,
    ILastOrderRepository lastOrderRepository,
    ICanceledOrderRepository canceledOrderRepository,
    IOrderPublisher orderPublisher) : IOrderApplicationService
{
    private readonly ICurrentOrderRepository _currentOrderRepository = currentOrderRepository;
    private readonly ILastOrderRepository _lastOrderRepository = lastOrderRepository;
    private readonly ICanceledOrderRepository _canceledOrderRepository = canceledOrderRepository;
    private readonly IOrderPublisher _orderPublisher = orderPublisher;

    /// <summary>
    /// Возвращает список текущих заказов.
    /// </summary>
    /// <param name="role">Роль пользователя.</param>
    /// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
    /// <returns>Список текущих заказов.</returns>
    public async Task<List<CurrentOrder>> GetCurrentOrders(RoleCode role, Guid sourceId)
    {
        var orders = await _currentOrderRepository.Get(role, sourceId);
        return orders.OrderByDescending(o => o.OrderDate).ToList();
    }

    /// <summary>
    /// Возвращает список истории заказов.
    /// </summary>
    /// <param name="role">Роль пользователя.</param>
    /// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
    /// <returns>Список истории заказов.</returns>
    public async Task<List<LastOrder>> GetHistoryOrders(RoleCode role, Guid sourceId)
    {
        var orders = await _lastOrderRepository.Get(role, sourceId);
        return orders.OrderByDescending(o => o.OrderDate).ToList();
    }

    /// <summary>
    /// Возвращает список отмененный заказов.
    /// </summary>
    /// <param name="role">Роль пользователя.</param>
    /// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
    /// <returns>Список отменненых заказов.</returns>
    public async Task<List<CanceledOrder>> GetCanceledOrders(RoleCode role, Guid sourceId)
    {
        var orders = await _canceledOrderRepository.Get(role, sourceId);
        return orders.OrderByDescending(o => o.OrderDate).ToList();
    }

    /// <summary>
    /// Возвращает список текущих заказов для магазина(со статусом готовится и ожидает клиента).
    /// </summary>
    /// <param name="role">Роль пользователя.</param>
    /// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
    /// <returns>Список текущих заказов для магазина с ограничением по статусу.</returns>
    public async Task<List<CurrentOrder>> GetStoreCurrentOrders(RoleCode role, Guid sourceId)
    {
        var orders = await _currentOrderRepository.Get(role, sourceId);
        return orders.Where(x => x.Status <= StatusCode.WaitingCourier).ToList();
    }

    /// <summary>
    /// Возвращает самый старый активный заказ для указанной роли и источника.
    /// </summary>
    /// <param name="role">Роль пользователя.</param>
    /// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
    /// <returns>Самый старый активный заказ, или null, если заказов нет.</returns>
    public async Task<CurrentOrder?> GetOldestActive(RoleCode role, Guid sourceId)
    {
        var orders = await _currentOrderRepository.Get(role, sourceId);
        var ordersWithStatus = orders.Where(x => x.Status <= StatusCode.Delivering).ToList();
        var oldestActive = ordersWithStatus
            .FirstOrDefault(x => x.OrderDate == ordersWithStatus.Min(o => o.OrderDate));
        return oldestActive;
    }

    /// <summary>
    /// Изменяет статус заказа для активных заказов. Публикует в Кафку.
    /// </summary>
    /// <param name="role">Роль пользователя.</param>
    /// <param name="status">Новый статус заказа.</param>
    /// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
    /// <param name="orderId">Идентификатор заказа.</param>
    public async Task ChangeStatusActive(RoleCode role, StatusCode status, Guid sourceId, Guid orderId)
    {
        await _currentOrderRepository.ChangeStatus(role, status, sourceId, orderId);
        if (status == StatusCode.WaitingCourier)
            await _currentOrderRepository.ChangeCookingDate(role, DateTime.UtcNow, sourceId, orderId);
        if (status == StatusCode.WaitingClient)
            await _currentOrderRepository.ChangeDeliveryDate(role, DateTime.UtcNow, sourceId, orderId);
        var order = await _currentOrderRepository.GetById(role, sourceId, orderId);
        await _orderPublisher.PublishOrderUpdate(order.ToPublishedOrder());
    }

    /// <summary>
    /// Завершает заказ для указанной роли, источника и идентификатора заказа. Удаляет из текущей таблицы и записывает в истоию. Публикует в Кафку.
    /// </summary>
    /// <remarks>Может сделать только клиент, когда статус "Ожидает клиента"</remarks>
    /// <param name="role">Роль пользователя.</param>
    /// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <exception cref="FailToChangeStatus">Исключение возникает, если заказ не имеет статуса "Ожидает клиента".</exception>
    public async Task ChangeStatusCompleted(RoleCode role, Guid sourceId, Guid orderId)
    {
        var order = await _currentOrderRepository.GetById(role, sourceId, orderId);
        if (role != RoleCode.Client)
            throw new FailToChangeStatus("Только клиент может завершить заказ", order.Status);
        if (order.Status != StatusCode.WaitingClient)
            throw new FailToChangeStatus("Клиент может завершить только приехавший заказ", order.Status);
        await _lastOrderRepository.Create(order);
        await _currentOrderRepository.Delete(role, sourceId, orderId);
        await _orderPublisher.PublishOrderUpdate(order.ToPublishedOrder());
    }

    /// <summary>
    /// Отменяет заказ для указанной роли, источника и идентификатора заказа. Удаляет из текущей таблицы и записывает в отмененные заказы. Публикует в Кафку.
    /// </summary>
    /// <param name="role">Роль пользователя.</param>
    /// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <param name="reasonOfCanceled">Причина отмены заказа.</param>
    /// <exception cref="FailToChangeStatus">Исключение возникает, если заказ имеет недопустимый статус для отмены этой ролью.</exception>
    public async Task ChangeStatusCanceled(RoleCode role, Guid sourceId, Guid orderId, string reasonOfCanceled)
    {
        var order = await _currentOrderRepository.GetById(role, sourceId, orderId);
        if (role == RoleCode.Courier && order.Status is not (StatusCode.WaitingCourier or StatusCode.Delivering))
            throw new FailToChangeStatus("Курьер может отменить только заказ ожидающий курьера или доставляющийся",
                order.Status);
        if (role == RoleCode.Store && order.Status is not StatusCode.Cooking)
            throw new FailToChangeStatus("Магазин может отменить только готовящийся заказ", order.Status);
        await _canceledOrderRepository.Create(order, reasonOfCanceled, role);
        await _currentOrderRepository.Delete(role, sourceId, orderId);
        await _orderPublisher.PublishOrderUpdate(order.ToPublishedOrder());
    }

    /// <summary>
    /// Возвращает список новых заказов после указанной даты.
    /// </summary>
    /// <param name="role">Роль пользователя.</param>
    /// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
    /// <param name="lastOrderDate">Дата последнего полученного заказа.</param>
    /// <returns>Список новых заказов.</returns>
    public async Task<List<CurrentOrder>> GetNewOrdersByDate(RoleCode role, Guid sourceId, DateTime lastOrderDate)
    {
        var orders = await _currentOrderRepository.Get(role, sourceId);
        var newestOrder = orders
            .Where(x => x.Status <= StatusCode.WaitingCourier)
            .Where(x => x.OrderDate > lastOrderDate)
            .OrderBy(x => x.OrderDate)
            .ToList();
        return newestOrder;
    }

    /// <summary>
    /// Возвращает статус заказа. 
    /// </summary>
    /// <param name="role">Роль пользователя.</param>
    /// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>Текущий статус заказа.</returns>
    public async Task<StatusCode> GetStatus(RoleCode role, Guid sourceId, Guid id)
    {
        return await _currentOrderRepository.GetStatus(role, sourceId, id);
    }

    /// <summary>
    /// Создает новый заказ и публикует его в кафку. 
    /// </summary>
    /// <param name="order">Новый заказ.</param>
    public async Task CreateCurrentOrder(CurrentOrder order)
    {
        await _currentOrderRepository.Create(order);
        await _orderPublisher.PublishOrderCreate(order.ToPublishedOrder());
    }
}