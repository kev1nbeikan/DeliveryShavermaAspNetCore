using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Models;

namespace OrderService.Domain.Abstractions;

/// <summary>
/// Репозиторий для взаимодействия с таблицей текущих заказов.
/// </summary>
public interface ICurrentOrderRepository
{
	/// <summary>
	/// Возвращает список текущих заказов для указанной роли и идентификатора источника.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (клиент, курьер, магазин).</param>
	/// <returns>Список текущих заказов.</returns>
	/// <exception cref="FailToUseOrderRepository">Исключение выбрасывается, при работе json.</exception>
	Task<List<CurrentOrder>> Get(RoleCode role, Guid sourceId);
	
	/// <summary>
	/// Возвращает информацию о заказе по его индефикатору.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (клиент, курьер, магазин).</param>
	/// <param name="id">Идентификатор заказа.</param>
	/// <returns>Текущий заказ.</returns>
	/// <exception cref="NotFoundOrder">Исключение выбрасывается, если заказ не найден.</exception>
	/// <exception cref="FailToUseOrderRepository">Исключение выбрасывается, при работе json.</exception>
	Task<CurrentOrder> GetById(RoleCode role, Guid sourceId, Guid id);
	
	/// <summary>
	/// Возвращает статус заказа.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (клиент, курьер, магазин).</param>
	/// <param name="id">Идентификатор заказа.</param>
	/// <returns>Статус заказа.</returns>
	/// <exception cref="NotFoundOrder">Исключение выбрасывается, если заказ не найден.</exception>
	Task<StatusCode> GetStatus(RoleCode role, Guid sourceId, Guid id);
	
	/// <summary>
	/// Создает новый текущий заказ.
	/// </summary>
	/// <param name="order">Объект текущего заказа.</param>
	/// <exception cref="FailToUseOrderRepository">Исключение выбрасывается, при работе json.</exception>
	Task Create(CurrentOrder order);
	
	/// <summary>
	/// Удаляет текущий заказ.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (клиент, курьер, магазин).</param>
	/// <param name="id">Идентификатор заказа.</param>
	Task Delete(RoleCode role, Guid sourceId, Guid id);
	
	/// <summary>
	/// Изменяет статус заказа.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="status">Новый статус заказа.</param>
	/// <param name="sourceId">Идентификатор источника (клиент, курьер, магазин).</param>
	/// <param name="id">Идентификатор заказа.</param>
	/// <exception cref="NotFoundOrder">Исключение выбрасывается, если заказ не найден.</exception>
	/// <exception cref="FailToChangeStatus">Исключение выбрасывается, если новый статус некорректен.</exception>
	Task ChangeStatus(RoleCode role, StatusCode status, Guid sourceId, Guid id);
	
	/// <summary>
	/// Изменяет дату приготовления заказа.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="cookingDate">Новая дата начала приготовления.</param>
	/// <param name="sourceId">Идентификатор источника (клиент, курьер, магазин).</param>
	/// <param name="id">Идентификатор заказа.</param>
	Task ChangeCookingDate(RoleCode role, DateTime cookingDate, Guid sourceId, Guid id);
	
	/// <summary>
	/// Изменяет дату доставки заказа.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="deliveryDate">Новая дата доставки.</param>
	/// <param name="sourceId">Идентификатор источника (клиент, курьер, магазин).</param>
	/// <param name="id">Идентификатор заказа.</param>
	Task ChangeDeliveryDate(RoleCode role, DateTime deliveryDate, Guid sourceId, Guid id);
}