using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Tests;
using Microsoft.Extensions.Options;
using Moq;
using StoreService.Application;
using StoreService.Core.Abstractions;
using StoreService.DataAccess.Repositories;

namespace StoreService.UnitTest;


// Нужно иметь запущенный сервис MenuService перед запуском тестов
public class StoreServiceMenuRepoTests
{
    private IMenuRepositoryApi _menuRepositoryApi;

    [SetUp]
    public void Setup()
    {
        var serviceOptions = Options.Create(new ServicesOptions());
        serviceOptions.Value.MenuUrl = "http://localhost:5002";

        var httpClientFactory = HttpClientFactoryMockBuilder.Create(serviceOptions)
            .WithMenuClient()
            .Build();

        _menuRepositoryApi = new MenuRepositoryApi(httpClientFactory, serviceOptions);
    }


    [Test]
    public async Task GetAll()
    {
        var menu = await _menuRepositoryApi.GetAll();
        Assert.IsNotNull(menu);
    }
}