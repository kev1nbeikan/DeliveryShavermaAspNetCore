using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Contracts.Orders;
using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using Handler.Core;
using Handler.Core.Abstractions.Services;
using HandlerService.Infustucture;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace HandlerService.UnitTests.Test;

public class OrderPublisherTest
{
    private readonly IOrderPublisher _publisher;

    public OrderPublisherTest()
    {
        var options = Options.Create(new KafkaOptions());
        options.Value.BootstrapServers = "http://localhost:29092/";

        _publisher = new OrderPublisher(options);
    }

    [Test]
    public async Task Publish()
    {
        await _publisher.PublishOrderCreate(new OrderCreateRequest
        (
            Id: Guid.NewGuid(),
            ClientId: Guid.NewGuid(),
            CourierId: Guid.NewGuid(),
            StoreId: Guid.NewGuid(),
            Basket: new List<BasketItem>
            {
                new BasketItem
                {
                    ProductId = Guid.NewGuid(),
                    Name = "test",
                    Amount = 1,
                    Price = 1
                }
            },
            Price: 100,
            Comment: "test",
            StoreAddress: "test",
            ClientAddress: "test",
            CourierNumber: "test",
            ClientNumber: "test",
            CookingTime: TimeSpan.FromSeconds(1),
            DeliveryTime: TimeSpan.FromSeconds(1),
            Cheque: "test"
        ));
    }
}