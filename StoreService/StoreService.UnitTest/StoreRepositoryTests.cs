using StoreService.Core;
using StoreService.Core.Abstractions;
using StoreService.DataAccess.Repositories;
using StoreService.UnitTest.Fixtures;
using StoreService.UnitTest.Utils;

namespace StoreService.UnitTest;

public class StoreRepositoryTests
{
    private IStoreRepository _storeRepository;


    [SetUp]
    public void Setup()
    {
        _storeRepository = new StoreRepository(DbContextFixture.Context);
    }

    [Test]
    public async Task AddAndGet()
    {
        var store = new Store
        {
            Id = Guid.NewGuid(),
            Status = StoreStatus.Open
        };

        await _storeRepository.Add(store);
        var storeFromRepo = await _storeRepository.Get(store.Id);

        Assert.That(storeFromRepo, Is.Not.Null);
        CustomAsserts.AssertAreEqual(store, storeFromRepo);
    }

    [Test]
    public async Task AddUpdateAndGet()
    {
        var store = new Store
        {
            Id = Guid.NewGuid(),
            Status = StoreStatus.Open
        };

        await _storeRepository.Add(store);
        store = await _storeRepository.Get(store.Id);
        store.Status = StoreStatus.Closed;

        var updateResult = await _storeRepository.Update(store);
        var storeFromRepo = await _storeRepository.Get(store.Id);

        Assert.That(updateResult, Is.True);
        CustomAsserts.AssertAreEqual(store, storeFromRepo);
    }
}