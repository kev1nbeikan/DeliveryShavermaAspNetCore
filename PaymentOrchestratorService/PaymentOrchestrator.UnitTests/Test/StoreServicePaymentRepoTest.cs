using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Models.Payment;
using BarsGroupProjectN1.Core.Tests;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Abstractions.Services;
using Handler.Core.Payment;
using HandlerService.DataAccess.Repositories;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace HandlerService.UnitTests.Test;
// надо запустить сервис магазина перед запуском тестов
public class StoreServicePaymentRepoTest
{
    private IStoreRepository _storeRepository;

    [SetUp]
    public void Setup()
    {
        var serviceOptions = Options.Create(new ServicesOptions());
        serviceOptions.Value.StoreUrl = "http://localhost:5249";

        var httpClientFactory = HttpClientFactoryMockBuilder.Create(serviceOptions)
            .WithStoreClient()
            .Build();

        _storeRepository = new StoreRepositoryHttp(httpClientFactory, serviceOptions);
    }


    [Test]
    public async Task GetAll()
    {
        var menu = await _storeRepository.GetCokingTime("ул Тест",
            [new ProductsInventory() { ProductId = Guid.Parse("ebf5fd27-fd27-44b3-8f48-0cf7a7211d3c"), Quantity = 1 }]);
        Assert.That(menu.error, Is.Null);
    }
}