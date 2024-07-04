using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Models;

namespace OrderService.Domain.Abstractions;

public interface IOrderPublisher
{
    Task PublishOrderCreate(PublishOrder order);
    Task PublishOrderUpdate(PublishOrder order);
}