using HandlerService.Controllers;

namespace Handler.Core.Abstractions.Services;

public interface IOrderService
{
    Task<string?> Save(Order order);

    public Task<(Order order, string? error)> CreateAndSave(Guid handlerServiceOrderId,
        Product[] orderBucket, int price,
        string comment, string cheque, string clientAddress, Curier curier, MyUser user, Guid storeId,
        TimeSpan cookingTime, TimeSpan deliveryTime);
}