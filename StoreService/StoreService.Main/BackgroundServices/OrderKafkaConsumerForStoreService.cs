using BarsGroupProjectN1.Core.BackgroundServices;
using BarsGroupProjectN1.Core.Contracts.Orders;
using StoreService.Core.Abstractions;

namespace StoreService.Main.BackgroundServices;

public class OrderKafkaConsumerForStoreService : OrderConsumerBackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IStoreService _storeService;

    public OrderKafkaConsumerForStoreService(ILogger<OrderKafkaConsumerForStoreService> logger,
        IConfiguration configuration, IServiceScopeFactory scopeFactory) : base(logger, configuration)
    {
        _scopeFactory = scopeFactory;
        _storeService = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IStoreService>();
    }

    protected override string GroupId()
    {
        return "Store";
    }

    protected override async Task ProcessOrder(OrderCreateRequest? order)
    {
        if (order != null)
        {
            try
            {
                await _storeService.IncreaseActiveOrdersCount(order.StoreId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        await base.ProcessOrder(order);
    }
}