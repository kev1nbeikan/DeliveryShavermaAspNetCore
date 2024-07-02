using BarsGroupProjectN1.Core.Contracts.Orders;

namespace Handler.Core.Abstractions.Services;

public interface IOrderPublisher
{
    Task PublishOrder(OrderCreateRequest order);
}