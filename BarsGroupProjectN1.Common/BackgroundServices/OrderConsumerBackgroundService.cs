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

    protected override void OnConfigure(ConsumerOptions consumerOptions)
    {
        consumerOptions.Topics = ["Orders", "OrdersUpdate"];
    }

    protected override async Task ProcessMessageAsync(string message)
    {
        try
        {
            var order = JsonSerializer.Deserialize<OrderCreateRequest>(message);
            if (order != null)
                await ProcessOrder(order);
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message, e);
        }

        return;
    }

    private void ProcessByTopic(string topic, string message)
        =>
            topic switch
            {
                "Orders" => ExecuteProcessingOfOrder(message),
                "OrdersUpdate" => ExecuteProcessingOfOrderUpdate(message),
                _ => throw new ArgumentOutOfRangeException()
            };


    private void ExecuteProcessingOfOrderUpdate(string message)
    {
        throw new NotImplementedException();
    }

    private void ExecuteProcessingOfOrder(string message)
    {
        throw new NotImplementedException();
    }

    protected virtual Task ProcessOrder(OrderCreateRequest order)
    {
        Logger.LogInformation("Get order {order}", order);
        return Task.CompletedTask;
    }
}