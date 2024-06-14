using Handler.Core;
using Handler.Core.Abstractions.Repositories;
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
        PaymentOrder paymentOrder = PaymentOrder.Create(
            Guid.NewGuid(),
            new Product[] { },
            100, "comment", "address", Guid.NewGuid(), Guid.NewGuid()).Order!;

        var error = _handlerRepository.Save(paymentOrder);
        var savedInMemoryPaymentOrder = _handlerRepository.Get(paymentOrder.Id);

        Assert.That(error, Is.Null);
        Assert.That(savedInMemoryPaymentOrder, Is.Not.Null);
        Assert.That(PaymentOrderIsEqual.IsEqual(paymentOrder, savedInMemoryPaymentOrder!), Is.True);
    }

    [Test]
    public void SavePaymentOrderInMemoryCasheAndDelete()
    {
        PaymentOrder paymentOrder = PaymentOrder.Create(
            Guid.NewGuid(),
            new Product[] { },
            100, "comment", "address", Guid.NewGuid(), Guid.NewGuid()).Order!;

        var error = _handlerRepository.Save(paymentOrder);
        var savedInMemoryPaymentOrder = _handlerRepository.Delete(paymentOrder.Id);

        Assert.That(error, Is.Null);
        Assert.That(savedInMemoryPaymentOrder, Is.Null);
    }
}