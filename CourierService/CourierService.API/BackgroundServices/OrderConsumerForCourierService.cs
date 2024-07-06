using BarsGroupProjectN1.Core.BackgroundServices;
using BarsGroupProjectN1.Core.Exceptions;
using BarsGroupProjectN1.Core.Models.Order;
using CourierService.Core.Abstractions;
using CourierService.Core.Exceptions;

namespace CourierService.API.BackgroundServices;

public class OrderConsumerForCourierService(
    ILogger<KafkaConsumerService> logger,
    IConfiguration configuration,
    IServiceScopeFactory scopeFactory)
    : OrderConsumerBackgroundService(logger, configuration)
{
    private readonly ICourierService _courierService =
        scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ICourierService>();

    protected override void OnConfigure(ConsumerOptions consumerOptions)
    {
        consumerOptions.GroupId = "CourierService";
        base.OnConfigure(consumerOptions);
    }

    protected override async Task ProcessOrderCreate(PublishOrder order)
    {
        await base.ProcessOrderCreate(order);
        try
        {
            await _courierService.OnOrderCreate(order);
        }
        catch (EntityNotFound e)
        {
            Logger.LogError(e, "Ошибка при обработке нового заказа");
        }
    }

    protected override async Task ProcessOrderUpdate(PublishOrder order)
    {
        await base.ProcessOrderCreate(order);
        try
        {
            await _courierService.OnOrderUpdate(order);
        }
        catch (EntityNotFound e)
        {
            Logger.LogError(e, "Ошибка при обработке нового заказа");
        }
        catch (RepositoryException e)
        {
            Logger.LogError(e, "Ошибка при обработке нового заказа");
        }
    }
}