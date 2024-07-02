using System.Text.Json;
using System.Text.Json.Serialization;
using BarsGroupProjectN1.Core.Contracts.Orders;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BarsGroupProjectN1.Core.BackgroundServices;

public abstract class OrderConsumerBackgroundService : KafkaConsumerService
{
    protected OrderConsumerBackgroundService(ILogger<KafkaConsumerService> logger, IConfiguration configuration) : base(
        logger, configuration)
    {
    }


    protected override string Topic()
    {
        return "Orders";
    }

    protected override async Task ProcessMessageAsync(string message)
    {
        var order = JsonSerializer.Deserialize<OrderCreateRequest>(message);
        await ProcessOrder(order);
        return;
    }

    protected virtual Task ProcessOrder(OrderCreateRequest? order)
    {
        if (order == null)
        {
            Logger.LogInformation("Order is null");
        }
        else
        {
            Logger.LogInformation("Get order {order}", order);
        }
        
        return Task.CompletedTask;
    }
}