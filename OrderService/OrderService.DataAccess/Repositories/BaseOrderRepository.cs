using System.Linq.Expressions;
using BarsGroupProjectN1.Core.Models;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Exceptions;

namespace OrderService.DataAccess.Repositories;

/// <summary>
/// Базовый класс для репозиториев заказов.
/// </summary>
public static class BaseOrderRepository
{
    /// <summary>
    /// Возвращает выражение для фильтрации заказов по роли и идентификатору источника.
    /// </summary>
    /// <typeparam name="T">Тип сущности заказа, наследующий от <see cref="BaseOrderEntity"/>.</typeparam>
    /// <param name="role">Роль пользователя.</param>
    /// <param name="sourceId">Идентификатор источника (клиент, курьер, магазин).</param>
    /// <returns>Выражение для фильтрации заказов.</returns>
    /// <exception cref="FailToUseOrderRepository">Исключение выбрасывается, если код роли некорректен.</exception>
    public static Expression<Func<T, bool>> GetCondition<T>(RoleCode role, Guid sourceId) where T : BaseOrderEntity
    {
        return role switch
        {
            RoleCode.Client => b => b.ClientId == sourceId,
            RoleCode.Courier => b => b.CourierId == sourceId,
            RoleCode.Store => b => b.StoreId == sourceId,
            _ => throw new FailToUseOrderRepository("Неправильный код роли")
        };
    }
}