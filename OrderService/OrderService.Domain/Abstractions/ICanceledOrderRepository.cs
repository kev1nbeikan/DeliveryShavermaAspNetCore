using BarsGroupProjectN1.Core.Models;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Models;

namespace OrderService.Domain.Abstractions;

/// <summary>
/// Репозиторий для взаимодействия с таблицей отмененных заказов.
/// </summary>
public interface ICanceledOrderRepository
{
	/// <summary>
	/// Возвращает список отмененных заказов для указанной роли и идентификатора источника.
	/// </summary>
	/// <param name="role">Роль пользователя.</param>
	/// <param name="sourceId">Идентификатор источника (клиент, курьер, магазин).</param>
	/// <returns>Список отмененных заказов.</returns>
	/// <exception cref="FailToUseOrderRepository">Исключение выбрасывается, при работе json.</exception>
	Task<List<CanceledOrder>> Get(RoleCode role, Guid sourceId);
	
	/// <summary>
	/// Создает новый отмененный заказ.
	/// </summary>
	/// <param name="order">Объект заказа.</param>
	/// <param name="reasonOfCanceled">Причина отмены заказа.</param>
	/// <param name="role">Роль пользователя, который отменил заказ.</param>
	/// <exception cref="FailToUseOrderRepository">Исключение выбрасывается, при работе json.</exception>
	Task Create(CurrentOrder order, string reasonOfCanceled, RoleCode role);
}