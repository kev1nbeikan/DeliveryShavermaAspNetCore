using System.Net;
using System.Text.Json;
using BarsGroupProjectN1.Core.Contracts.Orders;
using BarsGroupProjectN1.Core.Exceptions;
using Confluent.Kafka;
using Handler.Core.Abstractions.Services;

namespace HandlerService.Infustucture;

public class OrderPublisher : IOrderPublisher
{
    private readonly IProducer<Null, string> _producer;

    public OrderPublisher(IProducer<Null, string> producer)
    {
        ProducerConfig config = new()
        {
            BootstrapServers = "localhost:29092",
            ClientId = Dns.GetHostName()
        };
        
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task PublishOrder(OrderCreateRequest order)
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
}

