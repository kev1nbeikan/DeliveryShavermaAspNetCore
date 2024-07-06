using BarsGroupProjectN1.Core.BackgroundServices;
using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using StoreService.Core.Abstractions;

namespace StoreService.Main.BackgroundServices;

public class OrderKafkaConsumerForStoreService(
    ILogger<OrderKafkaConsumerForStoreService> logger,
    IConfiguration configuration,
    IServiceScopeFactory scopeFactory
) : OrderConsumerBackgroundService(logger, configuration)
{
    private readonly IStoreService _storeService =
        scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IStoreService>();

    protected override void OnConfigure(ConsumerOptions consumerOptions)
    {
        consumerOptions.GroupId = "Store";
        base.OnConfigure(consumerOptions);
    }


    protected override async Task ProcessOrderCreate(PublishOrder order)
    {
        await base.ProcessOrderCreate(order);
        try
        {
            await _storeService.OnOrderCreate(order);
        }
        catch (Exception e)
        {
            Logger.LogError(e, e.Message);
        }
    }

    protected override async Task ProcessOrderUpdate(PublishOrder order)
    {
        await base.ProcessOrderUpdate(order);
        try
        {
            await _storeService.OnOrderUpdate(order);
        }
        catch (Exception e)
        {
            Logger.LogError(e, e.Message);
        }
    }
}