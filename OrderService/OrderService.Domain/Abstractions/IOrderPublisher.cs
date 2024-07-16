using BarsGroupProjectN1.Core.Models.Order;

namespace OrderService.Domain.Abstractions;

/// <summary>
/// Сервис для публикации событий о заказах в Kafka.
/// </summary>
public interface IOrderPublisher
{
    Task PublishOrderCreate(PublishOrder order);
    Task PublishOrderUpdate(PublishOrder order);
}