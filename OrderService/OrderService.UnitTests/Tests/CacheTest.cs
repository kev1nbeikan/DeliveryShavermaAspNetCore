using Microsoft.Extensions.Caching.Distributed;
using NUnit.Framework;
using OrderService.DataAccess.Repositories;
using OrderService.UnitTests.Fixtures;

namespace OrderService.UnitTests.Tests;

// public class CacheTest
// {
//     private CurrentOrderRepository _currentOrderRepository;
//
//     [SetUp]
//     public void SetUp() => _currentOrderRepository = new CurrentOrderRepositoryWithCache(new CurrentOrderRepository(TestFixture.OrderServiceDbContext), new Cashe());
//
// }