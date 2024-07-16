using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Models.Order;
using NUnit.Framework;
using OrderService.DataAccess.Repositories;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Models;
using OrderService.UnitTests.Data;

namespace OrderService.UnitTests.Tests;

[TestFixture]
public class TestCreateModel
{
    public static IEnumerable<TestCaseData> GuidTestCases()
    {
        yield return new TestCaseData(Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty);
        yield return new TestCaseData(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.Empty);
        yield return new TestCaseData(Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, Guid.NewGuid());
        yield return new TestCaseData(Guid.NewGuid(), Guid.Empty, Guid.NewGuid(), Guid.NewGuid());
        yield return new TestCaseData(Guid.Empty, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
    }

    public static IEnumerable<TestCaseData> TimeTestCases()
    {
        yield return new TestCaseData( TimeSpan.FromMinutes(-45) , TimeSpan.FromMinutes(45));
        yield return new TestCaseData( TimeSpan.FromMinutes(45) , TimeSpan.FromMinutes(-45));
    }
    
    public static IEnumerable<TestCaseData> StringLengthTestCases()
    {
        yield return new TestCaseData( new string('a', 251) , "asd", "asd");
        yield return new TestCaseData( "asd" , new string('a', 251), "asd");
        yield return new TestCaseData( "asd" , "asd", new string('a', 501));
    }
    
    [Test]
    [TestCase("81234567890")]
    [TestCase("+71234567890")]
    [TestCase("+7(123)4567890")]
    public async Task CreateCurrent_CorrectData_Create(string number)
    {
        var createCurrentOrder = () => CurrentOrder.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            [new BasketItem { ProductId = Guid.NewGuid(), Name = "Product name", Amount = 1, Price = 10 }],
            100,
            "Some comment",
            "Store address",
            "Client address",
            number,
            number,
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "Some cheque",
            StatusCode.Cooking
        );

        Assert.DoesNotThrow(() => createCurrentOrder());
    }

    [Test]
    public async Task CreateCancel_CorrectData_Create()
    {
        var createCancelOrder = () => CanceledOrder.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            [new BasketItem { ProductId = Guid.NewGuid(), Name = "Product name", Amount = 1, Price = 10 }],
            100,
            "comment",
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "cheque",
            StatusCode.Cooking,
            "reasonOfCanceled",
            DateTime.UtcNow,
            RoleCode.Courier
        );

        Assert.DoesNotThrow(() => createCancelOrder());
    }
    
    [Test]
    public async Task CreateLast_CorrectData_Create()
    {
        var creatLastOrder = () => LastOrder.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            [new BasketItem { ProductId = Guid.NewGuid(), Name = "Product name", Amount = 1, Price = 10 }],
            100,
            "comment",
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "cheque"
        );

        Assert.DoesNotThrow(() => creatLastOrder());
    }
    
    [Test]
    public async Task CreateCurrent_CorrectMaxLengthForText_Create()
    {
        var createCurrentOrder = () => CurrentOrder.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            [new BasketItem { ProductId = Guid.NewGuid(), Name = "Product name", Amount = 1, Price = 10 }],
            100,
            new string('a', 499),
            new string('a', 249),
            new string('a', 249),
            "81234567890",
            "81234567890",
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            new string('a', 499),
            StatusCode.Cooking
        );

        Assert.DoesNotThrow(() => createCurrentOrder());
    }
    
    [Test, TestCaseSource(nameof(GuidTestCases))]
    public async Task CreateCurrent_EmptyId_Exception(Guid id, Guid clientId, Guid courierId, Guid storeId)
    {
        var createCurrentOrder = () => CurrentOrder.Create(
            id,
            clientId,
            courierId,
            storeId,
            [new BasketItem { ProductId = Guid.NewGuid(), Name = "Product name", Amount = 1, Price = 10 }],
            100,
            "Some comment",
            "Store address",
            "Client address",
            "81234567890",
            "81234567890",
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "Some cheque",
            StatusCode.Cooking
        );

        Assert.Throws<FailToCreateOrderModel>(() => createCurrentOrder());
    }

    [Test]
    public async Task CreateCurrent_EmptyBasket_Exception()
    {
        var createCurrentOrder = () => CurrentOrder.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            new List<BasketItem>(),
            100,
            "Some comment",
            "Store address",
            "Client address",
            "81234567890",
            "81234567890",
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "Some cheque",
            StatusCode.Cooking
        );

        Assert.Throws<FailToCreateOrderModel>(() => createCurrentOrder());
    }
    
    [Test]
    public async Task CreateCurrent_WrongPrice_Exception()
    {
        var createCurrentOrder = () => CurrentOrder.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            [new BasketItem { ProductId = Guid.NewGuid(), Name = "Product name", Amount = 1, Price = 10 }],
            -100,
            "Some comment",
            "Store address",
            "Client address",
            "81234567890",
            "81234567890",
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "Some cheque",
            StatusCode.Cooking
        );

        Assert.Throws<FailToCreateOrderModel>(() => createCurrentOrder());
    }

    [Test]
    [TestCase("","", "")]
    [TestCase("","asd", "asd")]
    [TestCase("asd","", "asd")]
    [TestCase("asd","asd", "")]
    [TestCase(null,"asd", "asd")]
    public async Task CreateCurrent_EmptyAddressAndCheque_Exception(string address1, string address2, string cheque)
    {
        var createCurrentOrder = () => CurrentOrder.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            [new BasketItem { ProductId = Guid.NewGuid(), Name = "Product name", Amount = 1, Price = 10 }],
            100,
            "Some comment",
            address1,
            address2,
            "81234567890",
            "81234567890",
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            cheque,
            StatusCode.Cooking
        );

        Assert.Throws<FailToCreateOrderModel>(() => createCurrentOrder());
    }
    
    [Test, TestCaseSource(nameof(TimeTestCases))]
    public async Task CreateCurrent_WrongTime_Exception(TimeSpan cookingTime, TimeSpan deliveryTime)
    {
        var createCurrentOrder = () => CurrentOrder.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            [new BasketItem { ProductId = Guid.NewGuid(), Name = "Product name", Amount = 1, Price = 10 }],
            100,
            "Some comment",
            "Some address",
            "Some address",
            "81234567890",
            "81234567890",
            cookingTime,
            deliveryTime,
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "Some cheque",
            StatusCode.Cooking
        );

        Assert.Throws<FailToCreateOrderModel>(() => createCurrentOrder());
    }
    
    
    [Test, TestCaseSource(nameof(StringLengthTestCases))]
    public async Task CreateCurrent_WrongLengthAddressAndCheque_Exception(string address1, string address2 ,string cheque)
    {
        var createCurrentOrder = () => CurrentOrder.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            [new BasketItem { ProductId = Guid.NewGuid(), Name = "Product name", Amount = 1, Price = 10 }],
            100,
            "Comment",
            address1,
            address2,
            "81234567890",
            "81234567890",
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            cheque,
            StatusCode.Cooking
        );

        Assert.Throws<FailToCreateOrderModel>(() => createCurrentOrder());
    }
    
    [Test]
    public async Task CreateCurrent_WrongLengthComment_Exception()
    {
        var createCurrentOrder = () => CurrentOrder.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            [new BasketItem { ProductId = Guid.NewGuid(), Name = "Product name", Amount = 1, Price = 10 }],
            100,
            new string('a', 501),
            "Store address desc",
            "Client address",
            "81234567890",
            "81234567890",
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "cheque",
            StatusCode.Cooking
        );

        Assert.Throws<FailToCreateOrderModel>(() => createCurrentOrder());
    }
    
    [Test]
    public async Task CreateCancel_WrongLastStatus_Exception()
    {
        var createCancelOrder = () => CanceledOrder.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            [new BasketItem { ProductId = Guid.NewGuid(), Name = "Product name", Amount = 1, Price = 10 }],
            100,
            "comment",
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "cheque",
            (StatusCode)5,
            "reasonOfCanceled",
            DateTime.UtcNow,
            RoleCode.Courier
        );

        Assert.Throws<FailToCreateOrderModel>(() => createCancelOrder());
    }
    
    [Test]
    public async Task CreateCancel_WrongWhoCanceled_Exception()
    {
        var createCancelOrder = () => CanceledOrder.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            [new BasketItem { ProductId = Guid.NewGuid(), Name = "Product name", Amount = 1, Price = 10 }],
            100,
            "comment",
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "cheque",
            (StatusCode)1,
            "reasonOfCanceled",
            DateTime.UtcNow,
            (RoleCode)15
        );

        Assert.Throws<FailToCreateOrderModel>(() => createCancelOrder());
    }
    
    [Test]
    public async Task CreateCancel_EmptyReasonOfCanceled_Exception()
    {
        var createCancelOrder = () => CanceledOrder.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            [new BasketItem { ProductId = Guid.NewGuid(), Name = "Product name", Amount = 1, Price = 10 }],
            100,
            "comment",
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "cheque",
            (StatusCode)1,
            "",
            DateTime.UtcNow,
            (RoleCode)1
        );

        Assert.Throws<FailToCreateOrderModel>(() => createCancelOrder());
    }
    
    [Test]
    public async Task CreateCancel_WrongMaxLengthReasonOfCanceled_Exception()
    {
        var createCancelOrder = () => CanceledOrder.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            [new BasketItem { ProductId = Guid.NewGuid(), Name = "Product name", Amount = 1, Price = 10 }],
            100,
            "comment",
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "cheque",
            (StatusCode)1,
            new string('a', 501),
            DateTime.UtcNow,
            (RoleCode)1
        );

        Assert.Throws<FailToCreateOrderModel>(() => createCancelOrder());
    }
}