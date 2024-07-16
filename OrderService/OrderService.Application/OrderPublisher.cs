using System.Net;
using System.Text.Json;
using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.BackgroundServices;
using BarsGroupProjectN1.Core.Exceptions;
using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrderService.Domain.Abstractions;

namespace OrderService.Application;

///<inheritdoc/>

public class OrderPublisher : IOrderPublisher
{
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<OrderPublisher> _logger;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="OrderPublisher"/>.
    /// </summary>
    /// <param name="options">Настройки Kafka.</param>
    /// <param name="logger">Логгер.</param>
    public OrderPublisher(IOptions<KafkaOptions> options, ILogger<OrderPublisher> logger)
    {
        _logger = logger;

        _logger.LogInformation(options.ToString());

        ProducerConfig config = new()
        {
            BootstrapServers = options.Value.BootstrapServers,
            ClientId = Dns.GetHostName()
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    /// <summary>
    /// Публикует событие о создании заказа в Kafka.
    /// </summary>
    /// <param name="order">Информация о созданном заказе.</param>
    /// <exception cref="RepositoryException">Исключение, которое выбрасывается при ошибке публикации.</exception>
    public async Task PublishOrderCreate(PublishOrder order)
    {
        try
        {
            _logger.LogInformation($"Publishing Order: {JsonSerializer.Serialize(order)}");
            await _producer.ProduceAsync(OrderConsumerBackgroundService.Topics.OrderCreateTopic,
                new Message<Null, string>
                {
                    Value = JsonSerializer.Serialize(order)
                });
            _logger.LogInformation($"Published Order successfully: {JsonSerializer.Serialize(order)}");
        }
        catch (ProduceException<Null, string> e)
        {
            throw new RepositoryException(e.Message);
        }
    }

    /// <summary>
    /// Публикует событие об обновлении заказа в Kafka.
    /// </summary>
    /// <param name="order">Информация об обновленном заказе.</param>
    /// <exception cref="RepositoryException">Исключение, которое выбрасывается при ошибке публикации.</exception>
    public async Task PublishOrderUpdate(PublishOrder order)
    {
        try
        {
            await _producer.ProduceAsync(OrderConsumerBackgroundService.Topics.OrderUpdateTopic,
                new Message<Null, string>
                {
                    Value = JsonSerializer.Serialize(order)
                });
        }
        catch (ProduceException<Null, string> e)
        {
            throw new RepositoryException(e.Message);
        }
    }
}