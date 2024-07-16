using System.Text.Json;
using BarsGroupProjectN1.Core.Models.Order;
using NUnit.Framework;
using OrderService.DataAccess.Repositories;
using OrderService.Domain.Models;
using OrderService.UnitTests.Data;
using OrderService.UnitTests.Fixtures;
using OrderService.UnitTests.Utils;

namespace OrderService.UnitTests.Tests;

[TestFixture]
public class OrderServiceRepositoryTest
{
    private CurrentOrderRepository _currentOrderRepository;

    [SetUp]
    public void SetUp() => _currentOrderRepository = new CurrentOrderRepository(TestFixture.OrderServiceDbContext);

    [Test]
    public async Task Create_CorrectData_CreateOrderAndReturnId()
    {
        var newOrder = TestCurrentOrderData.Correct;
        
        Console.WriteLine(newOrder.Basket.Count);
        // await _currentOrderRepository.Create(newOrder);
        // var order = await _currentOrderRepository.GetById(0, newOrder.ClientId, newOrder.Id);
        //
        // Assert.That(order.IsEqualOrder(newOrder), Is.True, 
        //     $"Заказ с идентификатором {newOrder.ClientId} " +
        //                                                     $"и клиентом {newOrder.Id} не найден в базе данных");
        //
    }
    
    // [Test]
    // public async Task Create_EmptyData_CreateOrderAndReturnId()
    // {
    //     var newOrder = TestCurrentOrderData.EmptyOrderData;
    //
    //     await _currentOrderRepository.Create(newOrder);
    //     var order = await _currentOrderRepository.GetById(0, newOrder.ClientId, newOrder.Id);
    //
    //     Console.WriteLine($"{JsonSerializer.Serialize(order)} + {order.Basket.Count}");
    //     Assert.That(order.IsEqualOrder(newOrder), Is.True, 
    //         $"Заказ с идентификатором {newOrder.ClientId} " +
    //         $"и клиентом {newOrder.Id} не найден в базе данных");
    // }
}