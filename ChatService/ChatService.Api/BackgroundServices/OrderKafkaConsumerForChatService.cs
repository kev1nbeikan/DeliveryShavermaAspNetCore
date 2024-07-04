using BarsGroupProjectN1.Core.BackgroundServices;
using BarsGroupProjectN1.Core.Contracts.Orders;
using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;

namespace ChatService.Api.BackgroundServices;

public class OrderKafkaConsumerForChatService : OrderConsumerBackgroundService
{
    public OrderKafkaConsumerForChatService(ILogger<OrderKafkaConsumerForChatService> logger,
        IConfiguration configuration) : base(logger, configuration)
    {
    }

    protected override void OnConfigure(ConsumerOptions consumerOptions)
    {
        consumerOptions.GroupId = "Chat";
        base.OnConfigure(consumerOptions);
    }

    protected override Task ProcessOrderCreate(PublishOrder order)
    {
        base.ProcessOrderCreate(order);
        return Task.CompletedTask;
    }
}