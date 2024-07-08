using System.Linq.Expressions;
using BarsGroupProjectN1.Core.Models;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Exceptions;

namespace OrderService.DataAccess.Repositories;

public static class BaseOrderRepository
{
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