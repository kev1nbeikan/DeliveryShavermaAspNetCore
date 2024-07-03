using BarsGroupProjectN1.Core.Models;
using OrderService.Domain.Models;

namespace OrderService.Domain.Abstractions;

public interface IOrderPublisher
{
    Task PublishOrderCreate(PublishOrder order);
    Task PublishOrderUpdate(PublishOrder order);
}