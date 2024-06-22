using Handler.Core;
using Handler.Core.Abstractions.Repositories;
using Handler.Core.Common;
using Handler.Core.HanlderService;
using HandlerService.DataAccess.Repositories;
using HandlerService.UnitTests.Utils;
using NUnit.Framework;

namespace HandlerService.UnitTests.Test;

public class PaymentRepositoryTest
{
    private IHandlerRepository _handlerRepository;

    [SetUp]
    public void Setup()
    {
        _handlerRepository = new HandlerRepository(TestFixture.InMemory());
    }

    [Test]
    public void SavePaymentOrderInMemoryCasheAndGetBack()
    {
        TemporyOrder temporyOrder = TemporyOrder.Create(
            Guid.NewGuid(),
            new Product[] { },
            100, "comment", "address", Guid.NewGuid(), Guid.NewGuid()).Order!;

        var error = _handlerRepository.Save(temporyOrder);
        var savedInMemoryPaymentOrder = _handlerRepository.Get(temporyOrder.Id);

        Assert.That(error, Is.Null);
        Assert.That(savedInMemoryPaymentOrder, Is.Not.Null);
        Assert.That(PaymentOrderIsEqual.IsEqual(temporyOrder, savedInMemoryPaymentOrder!), Is.True);
    }

    [Test]
    public void SavePaymentOrderInMemoryCasheAndDelete()
    {
        TemporyOrder temporyOrder = TemporyOrder.Create(
            Guid.NewGuid(),
            new Product[] { },
            100, "comment", "address", Guid.NewGuid(), Guid.NewGuid()).Order!;

        var error = _handlerRepository.Save(temporyOrder);
        var savedInMemoryPaymentOrder = _handlerRepository.Delete(temporyOrder.Id);

        Assert.That(error, Is.Null);
        Assert.That(savedInMemoryPaymentOrder, Is.Null);
    }
}