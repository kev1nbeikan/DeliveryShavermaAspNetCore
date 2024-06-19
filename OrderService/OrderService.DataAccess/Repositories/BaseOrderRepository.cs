using System.Linq.Expressions;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;
using OrderService.Domain.Models.Code;

namespace OrderService.DataAccess.Repositories;

public static class BaseOrderRepository
{
    public static Expression<Func<T, bool>> GetCondition<T>(RoleCode role, Guid sourceId) where T : BaseOrderEntity
    {
        switch (role)
        {
            case RoleCode.Client:
                return b => b.ClientId == sourceId;
            case RoleCode.Courier:
                return b => b.CourierId == sourceId;
            case RoleCode.Store:
                return b => b.StoreId == sourceId;
            default:
                throw new ArgumentException("Invalid RoleCode value");
        }
    }
}