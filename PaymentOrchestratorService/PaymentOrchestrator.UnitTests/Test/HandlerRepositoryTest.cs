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
    private IPaymentRepository _paymentRepository;

    [SetUp]
    public void Setup()
    {
        _paymentRepository = new PaymentRepository(TestFixture.InMemory());
    }

    [Test]
    public void SavePaymentOrderInMemoryCasheAndGetBack()
    {
        PaymentOrder paymentOrder = PaymentOrder.Create(
            Guid.NewGuid(),
            new Product[] { }, [],
            100, "comment", "address", Guid.NewGuid(), "+785781884").Order!;

        var error = _paymentRepository.Save(paymentOrder);
        var savedInMemoryPaymentOrder = _paymentRepository.Get(paymentOrder.Id);

        Assert.That(error, Is.Null);
        Assert.That(savedInMemoryPaymentOrder, Is.Not.Null);
        Assert.That(PaymentOrderIsEqual.IsEqual(paymentOrder, savedInMemoryPaymentOrder!), Is.True);
    }

    [Test]
    public void SavePaymentOrderInMemoryCasheAndDelete()
    {
        PaymentOrder paymentOrder = PaymentOrder.Create(
            Guid.NewGuid(),
            new Product[] { }, [],
            100, "comment", "address", Guid.NewGuid(), "+7857818489").Order!;

        var error = _paymentRepository.Save(paymentOrder);
        var savedInMemoryPaymentOrder = _paymentRepository.Delete(paymentOrder.Id);

        Assert.That(error, Is.Null);
        Assert.That(savedInMemoryPaymentOrder, Is.Null);
    }
}