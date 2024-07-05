using BarsGroupProjectN1.Core.Contracts.Orders;
using BarsGroupProjectN1.Core.Models;
using Handler.Core.Common;
using Handler.Core.Contracts;
using Handler.Core.HanlderService;
using UserService.Core;

namespace Handler.Core.Abstractions.Services;

public interface IOrderService
{

    public Task<(OrderCreateRequest order, string? error)> Save(PaymentOrder order, OrderLogistic orderLogistic,
        string cheque, MyUser user);
}