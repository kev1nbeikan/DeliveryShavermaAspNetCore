using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using BarsGroupProjectN1.Core.Contracts.Orders;
using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BarsGroupProjectN1.Core.BackgroundServices;

public abstract class OrderConsumerBackgroundService : KafkaConsumerService
{
    public static class Topics
    {
        public const string OrderCreateTopic = "OrdersCreated";
        public const string OrderUpdateTopic = "OrdersCancelled";
    }


    protected OrderConsumerBackgroundService(ILogger<KafkaConsumerService> logger, IConfiguration configuration) : base(
        logger, configuration)
    {
    }

    protected override void OnConfigure(ConsumerOptions consumerOptions)
    {
        consumerOptions.Topics = [Topics.OrderCreateTopic, Topics.OrderUpdateTopic];
    }

    protected override async Task ProcessMessageAsync(string message)
    {
        await ProcessByTopic(Context.Topic, message);
    }

    private async Task ProcessByTopic(string topic, string message)
    {
        switch (topic)
        {
            case Topics.OrderCreateTopic:
                await ExecuteProcessingOfOrderCreate(message);
                break;

            case Topics.OrderUpdateTopic:
                await ExecuteProcessingOfOrderUpdate(message);
                break;
        }
    }


    private async Task ExecuteProcessingOfOrderUpdate(string message)
    {
        try
        {
            var order = JsonSerializer.Deserialize<PublishOrder>(message);
            if (order != null)
                await ProcessOrderUpdate(order);
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message, e);
        }
    }

    private async Task ExecuteProcessingOfOrderCreate(string message)
    {
        try
        {
            var order = JsonSerializer.Deserialize<PublishOrder>(message);
            if (order != null)
                await ProcessOrderCreate(order);
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message, e);
        }
    }

    protected virtual Task ProcessOrderCreate(PublishOrder order)
    {
        Logger.LogInformation("Get orderCreate {order}", order);
        return Task.CompletedTask;
    }

    protected virtual Task ProcessOrderUpdate(PublishOrder order)
    {
        Logger.LogInformation("Get orderUpdate {order}", order);
        return Task.CompletedTask;
    }
}