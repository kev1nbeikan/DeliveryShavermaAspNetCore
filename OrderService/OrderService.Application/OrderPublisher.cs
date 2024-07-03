using System.Net;
using System.Text.Json;
using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Contracts.Orders;
using BarsGroupProjectN1.Core.Exceptions;
using BarsGroupProjectN1.Core.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using OrderService.Domain.Abstractions;

namespace OrderService.Application;

public class OrderPublisher : IOrderPublisher
{
    private readonly IProducer<Null, string> _producer;

    public OrderPublisher(IOptions<KafkaOptions> options)
    {
        Console.Write(options);

        ProducerConfig config = new()
        {
            BootstrapServers = options.Value.BootstrapServers,
            ClientId = Dns.GetHostName()
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task PublishOrderCreate(PublishOrder order)
    {
        try
        {
            await _producer.ProduceAsync("Orders", new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(order)
            });
        }
        catch (ProduceException<Null, string> e)
        {
            throw new RepositoryException(e.Message);
        }
    }

    public async Task PublishOrderUpdate(PublishOrder order)
    {
        try
        {
            await _producer.ProduceAsync("OrdersCreate", new Message<Null, string>
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