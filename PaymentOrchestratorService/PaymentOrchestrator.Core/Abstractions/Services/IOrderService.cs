using Handler.Core.Common;
using Handler.Core.Contracts;
using Handler.Core.HanlderService;

namespace Handler.Core.Abstractions.Services;

public interface IOrderService
{

    public Task<(PaymentOrder order, string? error)> Save(PaymentOrder order, OrderLogistic orderLogistic,
        string cheque, MyUser user);
}