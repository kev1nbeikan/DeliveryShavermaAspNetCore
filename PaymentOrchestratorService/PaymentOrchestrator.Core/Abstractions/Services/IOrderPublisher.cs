using BarsGroupProjectN1.Core.Contracts.Orders;

namespace Handler.Core.Abstractions.Services;

public interface IOrderPublisher
{
    Task PublishOrderCreate(OrderCreateRequest order);
    Task PublishOrderUpdate(Order order);
}