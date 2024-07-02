using BarsGroupProjectN1.Core.BackgroundServices;
using BarsGroupProjectN1.Core.Contracts.Orders;

namespace ChatService.Api.BackgroundServices;

public class OrderKafkaConsumerForChatService : OrderConsumerBackgroundService
{
    public OrderKafkaConsumerForChatService(ILogger<OrderKafkaConsumerForChatService> logger,
        IConfiguration configuration) : base(logger, configuration)
    {
    }

    protected override string GroupId()
    {
        return "Chat";
    }

    protected override Task ProcessOrder(OrderCreateRequest? order)
    {
        
        
        base.ProcessOrder(order);
        return Task.CompletedTask;
    }
}