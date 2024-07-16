using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Models;

namespace OrderService.Domain.Abstractions;

/// <summary>
/// Сервис для работы с заказами.
/// </summary>
public interface IOrderApplicationService
{
	/// <summary>
	/// Возвращает список текущих заказов.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
	/// <returns>Список текущих заказов.</returns>
	Task<List<CurrentOrder>> GetCurrentOrders(RoleCode role, Guid sourceId);
	
	/// <summary>
	/// Возвращает список истории заказов.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
	/// <returns>Список истории заказов.</returns>
	Task<List<LastOrder>> GetHistoryOrders(RoleCode role, Guid sourceId);
	
	/// <summary>
	/// Возвращает список отмененный заказов.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
	/// <returns>Список отменненых заказов.</returns>
	Task<List<CanceledOrder>> GetCanceledOrders(RoleCode role, Guid sourceId);
	
	/// <summary>
	/// Возвращает список текущих заказов для магазина(со статусом готовится и ожидает клиента).
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
	/// <returns>Список текущих заказов для магазина с ограничением по статусу.</returns>
	Task<List<CurrentOrder>> GetStoreCurrentOrders(RoleCode role, Guid sourceId);
	
	/// <summary>
	/// Возвращает самый старый активный заказ для указанной роли и источника.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
	/// <returns>Самый старый активный заказ, или null, если заказов нет.</returns>
	Task<CurrentOrder?> GetOldestActive(RoleCode role, Guid sourceId);
	
	/// <summary>
	/// Изменяет статус заказа для активных заказов. Публикует в Кафку.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="status">Новый статус заказа.</param>
	/// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
	/// <param name="orderId">Идентификатор заказа.</param>
	Task ChangeStatusActive(RoleCode role, StatusCode status, Guid sourceId, Guid orderId);
	
	/// <summary>
	/// Завершает заказ для указанной роли, источника и идентификатора заказа. Удаляет из текущей таблицы и записывает в истоию. Публикует в Кафку.
	/// </summary>
	/// <remarks>Может сделать только клиент, когда статус "Ожидает клиента"</remarks>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
	/// <param name="orderId">Идентификатор заказа.</param>
	/// <exception cref="FailToChangeStatus">Исключение возникает, если заказ не имеет статуса "Ожидает клиента".</exception>
	Task ChangeStatusCompleted(RoleCode role, Guid sourceId, Guid orderId);
	
	/// <summary>
	/// Отменяет заказ для указанной роли, источника и идентификатора заказа. Удаляет из текущей таблицы и записывает в отмененные заказы. Публикует в Кафку.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
	/// <param name="orderId">Идентификатор заказа.</param>
	/// <param name="reasonOfCanceled">Причина отмены заказа.</param>
	/// <exception cref="FailToChangeStatus">Исключение возникает, если заказ имеет недопустимый статус для отмены этой ролью.</exception>
	Task ChangeStatusCanceled(RoleCode role, Guid sourceId, Guid orderId, string reasonOfCanceled);


	/// <summary>
	/// Возвращает список новых заказов после указанной даты.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
	/// <param name="lastOrderDate">Дата последнего полученного заказа.</param>
	/// <returns>Список новых заказов.</returns>
	Task<List<CurrentOrder>> GetNewOrdersByDate(RoleCode role, Guid sourceId, DateTime lastOrderDate);
	
	/// <summary>
	/// Возвращает статус заказа. 
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (магазин, клиент и т.д.).</param>
	/// <param name="id">Идентификатор заказа.</param>
	/// <returns>Текущий статус заказа.</returns>
	Task<StatusCode> GetStatus(RoleCode role, Guid sourceId, Guid id);
	
	/// <summary>
	/// Создает новый заказ и публикует его в кафку. 
	/// </summary>
	/// <param name="order">Новый заказ.</param>
	Task CreateCurrentOrder(CurrentOrder order);
}