using BarsGroupProjectN1.Core.Models;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Models;

namespace OrderService.Domain.Abstractions;

/// <summary>
/// Репозиторий для взаимодействия с таблицей истории заказов.
/// </summary>
public interface ILastOrderRepository
{
	/// <summary>
	/// Возвращает список прошлых заказов для указанной роли и идентификатора источника.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (клиент, курьер, магазин).</param>
	/// <returns>Список прошлых заказов.</returns>
	/// <exception cref="FailToUseOrderRepository">Исключение выбрасывается, при работе json.</exception>
	Task<List<LastOrder>> Get(RoleCode role, Guid sourceId);
	
	/// <summary>
	/// Создает новый заказ в историю.
	/// </summary>
	/// <param name="order">Объект заказа.</param>
	/// <exception cref="FailToUseOrderRepository">Исключение выбрасывается, при работе json.</exception>
	Task Create(CurrentOrder order);
}