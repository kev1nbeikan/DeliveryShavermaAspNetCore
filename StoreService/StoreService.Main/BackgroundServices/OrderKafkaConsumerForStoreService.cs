using BarsGroupProjectN1.Core.BackgroundServices;
using BarsGroupProjectN1.Core.Contracts.Orders;
using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using StoreService.Core.Abstractions;

namespace StoreService.Main.BackgroundServices;

public class OrderKafkaConsumerForStoreService : OrderConsumerBackgroundService
{
    private readonly IStoreService _storeService;

    public OrderKafkaConsumerForStoreService(ILogger<OrderKafkaConsumerForStoreService> logger,
        IConfiguration configuration, IServiceScopeFactory scopeFactory) : base(logger, configuration)
    {
        _storeService = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IStoreService>();
    }

    protected override void OnConfigure(ConsumerOptions consumerOptions)
    {
        consumerOptions.GroupId = "Store";
        base.OnConfigure(consumerOptions);
    }


    protected override async Task ProcessOrderCreate(PublishOrder order)
    {
        try
        {
            await _storeService.OnOrderCreate(order);
        }
        catch (Exception e)
        {
            Logger.LogError(e, e.Message);
        }

        await base.ProcessOrderCreate(order);
    }

    protected override async Task ProcessOrderUpdate(PublishOrder order)
    {
        try
        {
            await _storeService.OnOrderUpdate(order);
        }
        catch (Exception e)
        {
            Logger.LogError(e, e.Message);
        }
        await base.ProcessOrderUpdate(order);
    }
}